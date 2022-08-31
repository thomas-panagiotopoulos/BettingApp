using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SharedModel;
using BettingApp.Services.MatchSimulation.Domain.Events;
using BettingApp.Services.MatchSimulation.Domain.Exceptions;
using BettingApp.Services.MatchSimulation.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate
{
    public class Match : Entity, IAggregateRoot
    {
        // Id

        public string SimulationId => _simulationId;
        private string _simulationId;

        public string HomeClubId => _homeClubId;
        private string _homeClubId;

        public string HomeClubName => _homeClubName;
        private string _homeClubName;

        public string AwayClubId => _awayClubId;
        private string _awayClubId;

        public string AwayClubName => _awayClubName;
        private string _awayClubName;

        public League League { get; private set; }

        public int LeagueId => _leagueId;
        private int _leagueId;

        public string LeagueName => _leagueName;
        private string _leagueName;

        public DateTime KickoffDateTime => _kickoffDateTime;
        private DateTime _kickoffDateTime;

        public string CurrentMinute => _currentMinute;
        private string _currentMinute;

        public int HomeClubScore => _homeClubScore;
        private int _homeClubScore;

        public int AwayClubScore => _awayClubScore;
        private int _awayClubScore;

        public bool IsCanceled => _isCanceled;
        private bool _isCanceled;

        public IReadOnlyCollection<PossiblePick> PossiblePicks => _possiblePicks;
        private readonly List<PossiblePick> _possiblePicks;

        protected Match()
        {
            _currentMinute = "0";
            _homeClubScore = 0;
            _awayClubScore = 0;
            _isCanceled = false;
        }

        public Match(string id, string simulationId, Club homeClub, Club awayClub, int leagueId, DateTime kickoffDateTime, List<PossiblePick> possiblePicks) 
            : this()
        {
            Id = id; // we use pre-created Id (uniqueness is guaranteed by application service)
            _simulationId = simulationId;
            _homeClubId = homeClub.Id;
            _homeClubName = homeClub.Name;
            _awayClubId = awayClub.Id;
            _awayClubName = awayClub.Name;
            _leagueId = League.From(leagueId).Id;
            _leagueName = League.From(leagueId).Name;
            _kickoffDateTime = kickoffDateTime;

            _possiblePicks = possiblePicks;

            // domain level validation
            ValidatePossiblePicks(); 
            EnsurePossiblePicksBelongToMatch();

            // Add a MatchCreatedDomainEvent
            AddDomainEvent(new MatchCreatedDomainEvent(this.Id));
        }

        private void ValidatePossiblePicks()
        {
            if (_possiblePicks.Count() != MatchResult.List().Count())
                throw new MatchSimulationDomainException("PossiblePicks count doesn't match possible MatchResults count.");

            foreach (var result in MatchResult.List())
            {
                if (_possiblePicks.Where(p => p.MatchResultId == result.Id).Count() != 1)
                    throw new MatchSimulationDomainException("PossiblePicks don't contain exactly one PossiblePick for each possible MatchResult.");
            }
        }

        private void EnsurePossiblePicksBelongToMatch()
        {
            if (PossiblePicks.Any(pp => !pp.MatchId.Equals(this.Id)))
                throw new MatchSimulationDomainException("Not all PossiblePicks have the same MatchId with parent Match.");
        }

        private PossiblePick FindPossiblePick(int matchResultId)
        {
            var possiblePick = PossiblePicks.SingleOrDefault(p => p.MatchResultId == matchResultId);

            return possiblePick;
        }

        public void UpdatePossiblePickOdd(int matchResultId, decimal newOdd)
        {
            if (_isCanceled)
            {
                throw new MatchSimulationDomainException("Cannot update possible picks of a canceled match.");
            }

            var possiblePick = FindPossiblePick(matchResultId);

            if (possiblePick == null)
                throw new MatchSimulationDomainException($"PossiblePick with ResultId:{matchResultId} was not found in the Match (this shouldn't have happened).");

            possiblePick.UpdateOdd(newOdd);

        }

        public void DisablePossiblePick(int matchResultId)
        {
            if (_isCanceled)
            {
                throw new MatchSimulationDomainException("Cannot update possible picks of a canceled match.");
            }

            var possiblePick = FindPossiblePick(matchResultId);

            if (possiblePick == null)
                throw new MatchSimulationDomainException($"PossiblePick with ResultId:{matchResultId} was not found in the Match (this shouldn't have happened).");


            possiblePick.Disable();
        }

        public void EnablePossiblePick(int matchResultId)
        {
            if (_isCanceled)
            {
                throw new MatchSimulationDomainException("Cannot update possible picks of a canceled match.");
            }

            var possiblePick = FindPossiblePick(matchResultId);

            if (possiblePick == null)
                throw new MatchSimulationDomainException($"PossiblePick with ResultId:{matchResultId} was not found in the Match (this shouldn't have happened).");


            possiblePick.Enable();
        }

        // method that updates both Odd value and IsDisabled status of the child PossiblePick at once
        public void UpdatePossiblePick(int matchResultId, decimal newOdd, bool isDisabled)
        {
            if (_isCanceled)
            {
                throw new MatchSimulationDomainException("Cannot update possible picks of a canceled match.");
            }

            var possiblePick = FindPossiblePick(matchResultId);

            if (possiblePick == null)
                throw new MatchSimulationDomainException($"PossiblePick with ResultId:{matchResultId} was not found in the Match (this shouldn't have happened).");

            possiblePick.Update(newOdd, isDisabled);
        }

        // method that update Match's current minute
        public void UpdateCurrentMinute(string newCurrentMinute)
        {
            if (_isCanceled)
            {
                throw new MatchSimulationDomainException("Cannot update current minute of a canceled match.");
            }

            if (!OldAndNewCurrentMinuteAreSuccesive(_currentMinute, newCurrentMinute))
            {
                throw new MatchSimulationDomainException("Old and new current minutes are not succesive.");
            }

            _currentMinute = newCurrentMinute;

            // Add a MatchCurrentMinuteChangedDomainEvent
            AddDomainEvent(new MatchCurrentMinuteChangedDomainEvent(this.Id, _currentMinute));
        }

        // method that update Match's scores
        public void UpdateScores(int newHomeClubScore, int newAwayClubScore)
        {
            if (_isCanceled)
            {
                throw new MatchSimulationDomainException("Cannot update scores of a canceled match.");
            }

            if (!OldAndNewScoresAreSuccesive(HomeClubScore, AwayClubScore,
                                                      newHomeClubScore, newAwayClubScore))
            {
                throw new MatchSimulationDomainException("Old and new scores are not succesive.");
            }

            _homeClubScore = newHomeClubScore;
            _awayClubScore = newAwayClubScore;

            // Add a MatchScoresChangedDomainEvent
            AddDomainEvent(new MatchScoresChangedDomainEvent(this.Id, _homeClubScore, _awayClubScore));
        }

        // method that Cancels the Match (and the child PossiblePicks as well)
        public void Cancel()
        {
            if (_isCanceled)
            {
                throw new MatchSimulationDomainException("Cannot cancel a match that is already canceled.");
            }

            _isCanceled = true;

            _possiblePicks.ForEach(p => p.Cancel());

            // Add a MatchCanceledDomainEvent
            AddDomainEvent(new MatchCanceledDomainEvent(this.Id));

        }
        
        // method for business rules checking
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

        // method for business rules checking
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
