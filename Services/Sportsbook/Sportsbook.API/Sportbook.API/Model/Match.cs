using BettingApp.Services.Sportbook.API.Infrastructure.Exceptions;
using BettingApp.Services.Sportbook.API.Model.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Model
{
    public class Match : IEntity
    {
        public string Id { get; private set; }

        public string HomeClubName { get; private set; }

        public string AwayClubName { get; private set; }

        public League League { get; private set; }

        public int LeagueId { get; private set; }

        public string LeagueName { get; private set; }

        public DateTime KickoffDateTime { get; private set; }

        public string CurrentMinute { get; private set; }

        public int HomeClubScore { get; private set; }

        public int AwayClubScore { get; private set; }

        public bool IsCanceled { get; private set; }

        public bool IsBetable { get; private set; }

        public List<PossiblePick> PossiblePicks { get; private set; }

        protected Match()
        {
            IsCanceled = false;
        }

        public Match(string id, string homeClubName, string awayClubName, int leagueId, DateTime kickoffDateTime,
                     string currentMinute, int homeClubScore, int awayClubScore, List<PossiblePick> possiblePicks) 
            : this()
        {
            Id = id;
            HomeClubName = homeClubName;
            AwayClubName = awayClubName;
            LeagueId = League.From(leagueId).Id;
            LeagueName = League.From(leagueId).Name;
            KickoffDateTime = kickoffDateTime;
            CurrentMinute = currentMinute;
            HomeClubScore = homeClubScore;
            AwayClubScore = awayClubScore;
            PossiblePicks = possiblePicks;

            CalculateBetableStatus();

            ValidatePossiblePicks(); // domain-model level validation
            EnsurePossiblePicksBelongToMatch(); // not necessary as we use the same MatchId both on Match constructor and 
                                                // PossiblePick constructor as well (in MatchCreatedIntegrationEventHandler)
        }

        private void ValidatePossiblePicks()
        {
            if (PossiblePicks.Count() != MatchResult.List().Count())
                throw new SportsbookDomainException("PossiblePicks count doesn't match possible MatchResults count.");

            foreach (var result in MatchResult.List())
            {
                if(PossiblePicks.Where(p => p.MatchResultId == result.Id).Count() != 1)
                    throw new SportsbookDomainException("PossiblePicks don't contain exactly one PossiblePick for each possible MatchResult.");
            }
        }

        private void EnsurePossiblePicksBelongToMatch()
        {
            if (PossiblePicks.Any(pp => !pp.MatchId.Equals(this.Id)))
                throw new SportsbookDomainException("Not all PossiblePicks have the same MatchId with parent Match.");
        }

        public PossiblePick FindPossiblePick(int matchResultId)
        {
            var possiblePick = PossiblePicks.SingleOrDefault(p => p.MatchResultId == matchResultId);

            return possiblePick;
        }

        public void UpdatePossiblePickOdd(int matchResultId, decimal newOdd)
        {
            if (IsCanceled)
            {
                throw new SportsbookDomainException("Cannot update possible picks of a canceled match.");
            }

            var possiblePick = FindPossiblePick(matchResultId);

            if (possiblePick == null)
                throw new SportsbookDomainException($"PossiblePick with ResultId:{matchResultId} was not found in the Match (this shouldn't have happened).");

            possiblePick.UpdateOdd(newOdd);

        }

        public void DisablePossiblePick(int matchResultId)
        {
            if (IsCanceled)
            {
                throw new SportsbookDomainException("Cannot update possible picks of a canceled match.");
            }

            var possiblePick = FindPossiblePick(matchResultId);

            if (possiblePick == null)
                throw new SportsbookDomainException($"PossiblePick with ResultId:{matchResultId} was not found in the Match (this shouldn't have happened).");


            possiblePick.Disable();
        }

        public void EnablePossiblePick(int matchResultId)
        {
            if (IsCanceled)
            {
                throw new SportsbookDomainException("Cannot update possible picks of a canceled match.");
            }

            // if Match is not betable, then don't allow any of the child PossiblePicks to be enabled
            if (!this.IsBetable)
                return;

            var possiblePick = FindPossiblePick(matchResultId);

            if (possiblePick == null)
                throw new SportsbookDomainException($"PossiblePick with ResultId:{matchResultId} was not found in the Match (this shouldn't have happened).");


            possiblePick.Enable();
        }

        public void UpdatePossiblePick(int matchResultId, decimal newOdd, bool isDisabled)
        {
            if (IsCanceled)
            {
                throw new SportsbookDomainException("Cannot update possible picks of a canceled match.");
            }

            var possiblePick = FindPossiblePick(matchResultId);

            if (possiblePick == null)
                throw new SportsbookDomainException($"PossiblePick with ResultId:{matchResultId} was not found in the Match (this shouldn't have happened).");

            // If Match is not betable, this means it's either in final minutes or canceled, and there's no way to
            // become betable again in the future. So, we have to force all the children PossiblePicks to stay
            // not betable too.
            if (!this.IsBetable)
                isDisabled = true;

            possiblePick.Update(newOdd,isDisabled);
        }

        public void UpdateCurrentMinute(string newCurrentMinute)
        {
            if (IsCanceled)
            {
                throw new SportsbookDomainException("Cannot update current minute of a canceled match.");
            }

            if (!OldAndNewCurrentMinuteAreSuccesive(CurrentMinute,newCurrentMinute))
            {
                throw new SportsbookDomainException("Old and new current minutes are not succesive.");
            }

            CurrentMinute = newCurrentMinute;

            CalculateBetableStatus();

            // if the Match becomes not betable after a CurrentMinute change, then all the childern
            // PossiblePicks should get Disabled.
            if (!this.IsBetable)
            {
                PossiblePicks.ForEach(pp => pp.Disable());
            }
        }

        public void UpdateScores(int newHomeClubScore, int newAwayClubScore)
        {
            if (IsCanceled)
            {
                throw new SportsbookDomainException("Cannot update scores of a canceled match.");
            }

            if (!OldAndNewScoresAreSuccesive(HomeClubScore, AwayClubScore, 
                                                      newHomeClubScore, newAwayClubScore))
            {
                throw new SportsbookDomainException("Old and new scores are not succesive.");
            }

            HomeClubScore = newHomeClubScore;
            AwayClubScore = newAwayClubScore;
        }

        public void Cancel()
        {
            if (IsCanceled)
            {
                throw new SportsbookDomainException("Cannot cancel a match that is already canceled.");
            }

            IsCanceled = true;
            CalculateBetableStatus();

            // if the Match gets canceled, then all the childern PossiblePicks should get canceled too.
            PossiblePicks.ForEach(p => p.Cancel());
        }

        private void CalculateBetableStatus()
        {
            // regex that matches strings in format: "90", "90+1", "90+2", "90+3"..., "FT"
            var regex = new Regex("^(([9][0]([+][1-9][0-9]*)?)|([F][T]))$");
            var matchInFinalMinutes = regex.IsMatch(CurrentMinute);

            IsBetable = !IsCanceled && !matchInFinalMinutes;
        }

        private bool OldAndNewScoresAreSuccesive(int oldHomeClubScore, int oldAwayClubScore,
                                                              int newHomeClubScore, int newAwayClubScore)
        {
            if ((newHomeClubScore == oldHomeClubScore + 1 && newAwayClubScore == oldAwayClubScore) ||
                 (newHomeClubScore == oldHomeClubScore && newAwayClubScore == oldAwayClubScore + 1))
            {
                return true;
            }

            return false;
        }

        private bool OldAndNewCurrentMinuteAreSuccesive(string oldMinute, string newMinute)
        {
            if (oldMinute.Equals("HT"))
            {
                // Half Time
                if (newMinute.Equals("46"))
                {
                    // Only accept minute 46 if we are at Half Time
                    return true;
                }
            }
            else if (oldMinute.Equals("FT"))
            {
                // Full Time (this case should never be used because no more minute changes
                // should happen after a match is completed
                return false;
            }
            else if (oldMinute.Contains("+"))
            {
                // Extra Minutes (either first or second half)
                if (newMinute.Contains("+"))
                {
                    // Accept next Extra minute if we already are in Extra minutes, e.g. from 45+1 to 45+2
                    var intOldExtraMinutes = Int32.Parse(oldMinute.Split("+").Last());
                    var intOldStandardMinutes = Int32.Parse(oldMinute.Split("+").First());
                    var intNewExtraMinutes = Int32.Parse(newMinute.Split("+").Last());
                    var intNewStandardMinutes = Int32.Parse(newMinute.Split("+").First());

                    if ((intOldStandardMinutes == intNewStandardMinutes) &&
                         (intOldExtraMinutes + 1 == intNewExtraMinutes))
                    {
                        return true;
                    }

                }
                else if (newMinute.Equals("HT") && oldMinute.Split("+").First().Equals("45"))
                {
                    // Accept HT if we are in First Half Extra Minutes, e.g. from 45+3 to HT
                    return true;

                }
                else if (newMinute.Equals("FT") && oldMinute.Split("+").First().Equals("90"))
                {
                    // Accept FT if we are in Second Half Extra Minutes, e.g. from 90+4 to FT
                    return true;
                }
            }
            else
            {
                // Standard Minutes (either first or second half)
                if (!newMinute.Contains("+") &&
                     !newMinute.Equals("HT") &&
                     !newMinute.Equals("FT"))
                {
                    // Accept next Standard minute if we already are in Standard minutes,
                    // e.g. from 57 to 58 or from 0 to 1
                    var intOldMinute = Int32.Parse(oldMinute);
                    var intNewMinute = Int32.Parse(newMinute);

                    if (intOldMinute + 1 == intNewMinute)
                    {
                        return true;
                    }
                }
                else if (newMinute.Contains("+"))
                {
                    // Accept the first Extra minute if we are in the last Standard minute
                    // of the First or Second Hald, e.g. from 45 to 45+1 or from 90 to 90+1
                    var newStandardMinute = newMinute.Split("+").First();
                    var newExtraMinute = newMinute.Split("+").Last();

                    // we don't need to check if the newStandsMinute is equal to either "45" or "90"
                    // since we do this check in Validation behavior and thus we expect that
                    // when the newMinute contains a "+" then the newStandardMinute will always be
                    // either "45" or "90"
                    if (newStandardMinute.Equals(oldMinute) && newExtraMinute.Equals("1"))
                    {
                        return true;
                    }
                }
                else if (newMinute.Equals("HT") && oldMinute.Equals("45"))
                {
                    // Accept HT if we are in 45th minute
                    return true;

                }
                else if (newMinute.Equals("FT") && oldMinute.Equals("90"))
                {
                    // Accept FT if we are in 90th minute
                    return true;
                }
            }

            // if we got this far, old and new minute values contradict with each other
            return false;
        }
        
    }
}
