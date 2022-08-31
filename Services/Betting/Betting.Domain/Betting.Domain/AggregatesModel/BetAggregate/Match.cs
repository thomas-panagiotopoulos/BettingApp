using BettingApp.Services.Betting.Domain.Events;
using BettingApp.Services.Betting.Domain.Seedwork;
using BettingApp.Services.Betting.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate
{
    public class Match : Entity
    {
        // Id

        public string SelectionId => _selectionId;
        private string _selectionId;


        public string RelatedMatchId => _relatedMatchId;
        private string _relatedMatchId;


        public string HomeClubName => _homeClubName;
        private string _homeClubName;


        public string AwayClubName => _awayClubName;
        private string _awayClubName;

        public DateTime KickoffDateTime => _kickoffDateTime;
        private DateTime _kickoffDateTime;


        public string CurrentMinute => _currentMinute;
        private string _currentMinute;


        public int HomeClubScore => _homeClubScore;
        private int _homeClubScore;


        public int AwayClubScore => _awayClubScore;
        private int _awayClubScore;


        public Status Status { get; private set; }
        public int StatusId => _statusId;
        private int _statusId;
        public string StatusName => _statusName;
        private string _statusName;


        public MatchResult Result { get; private set; }
        public int WinnerResultId => _winnerResultId;
        private int _winnerResultId;
        public string WinnerResultName => _winnerResultName;
        private string _winnerResultName;
        public int GoalsResultId => _goalsResultId;
        private int _goalsResultId;
        public string GoalsResultName => _goalsResultName;
        private string _goalsResultName;                     



        // constructors

        public Match() { }

        public Match(string selectionId, string relatedMatchId, string homeClubName, string awayClubName, 
                     DateTime kickoffDateTime, string currentMinute, int homeClubScore, int awayClubScore)
        {
            _selectionId = selectionId;
            Id = CalculateUniqueIdForMatch();

            _relatedMatchId = relatedMatchId;
            _homeClubName = homeClubName;
            _awayClubName = awayClubName;
            _kickoffDateTime = kickoffDateTime;

            _currentMinute = currentMinute;
            _homeClubScore = homeClubScore;
            _awayClubScore = awayClubScore;

            // Calculate MatchStatus here
            CalculateStatus();

            // Calculate MatchResults here    
            CalculateResults();
        }


        // class methods implementing business logic

        private void CalculateStatus()
        {
            if (_currentMinute.Equals("FT"))
            {
                _statusId = Status.Completed.Id;
                _statusName = Status.Completed.Name;
            }
            else if (_currentMinute.Equals("0"))
            {
                _statusId = Status.Pending.Id;
                _statusName = Status.Pending.Name;
            }
            else
            {
                // some possible values in this case: "1", "2", "45", "45+1", "HT", "46", "90", "90+1"
                _statusId = Status.Ongoing.Id;
                _statusName = Status.Ongoing.Name;
            }
        }


        private void CalculateResults()
        {
            // calculate Winner Match Result
            if (_homeClubScore > _awayClubScore)
            {
                _winnerResultId = MatchResult.WinnerHomeClub.Id;
                _winnerResultName = MatchResult.WinnerHomeClub.Name;
            }
            else if (_homeClubScore == _awayClubScore)
            {
                _winnerResultId = MatchResult.WinnerDraw.Id;
                _winnerResultName = MatchResult.WinnerDraw.Name;
            }
            else if (_homeClubScore < _awayClubScore)
            {
                _winnerResultId = MatchResult.WinnerAwayClub.Id;
                _winnerResultName = MatchResult.WinnerAwayClub.Name;
            }

            // calculate Goals Match Result
            if (_homeClubScore + _awayClubScore <= 2)
            {
                _goalsResultId = MatchResult.GoalsUnder.Id;
                _goalsResultName = MatchResult.GoalsUnder.Name;
            }
            else if (_homeClubScore + _awayClubScore >= 3)
            {
                _goalsResultId = MatchResult.GoalsOver.Id;
                _goalsResultName = MatchResult.GoalsOver.Name;
            }
        }

        
        // method to be invoked by parent Bet's UpdateMatchCurrentMinute
        public void UpdateCurrentMinute(string newCurrentMinute)
        {
            if (_statusId == Status.Completed.Id || _statusId == Status.Canceled.Id)
            {
                throw new BettingDomainException("Match.UpdateCurrentMinute: " +
                    $"Cannot update current minute of a match with \"{_statusName}\" status.");
            }

            if (!OldAndNewCurrentMinuteAreSuccesive(_currentMinute, newCurrentMinute))
            {
                throw new BettingDomainException("Match.UpdateCurrentMinute: "+
                    "Old and new minute values are not succesive. "+
                    $"Old minute: {_currentMinute}, New minute: {newCurrentMinute}");
            }

            _currentMinute = newCurrentMinute;
            // Add a MatchCurrentMinuteChangedDomainEvent (for the SignalR service)
            AddDomainEvent(new MatchCurrentMinuteChangedDomainEvent(this.Id));

            var oldStatusId = _statusId;
            CalculateStatus();

            if (_statusId != oldStatusId)
            {
                // Add a MatchStatusChangedDomainEvent to queue to be dispatched later
                AddDomainEvent(new MatchStatusChangedDomainEvent(this.Id));
            }

        }

        // helper method used by UpdateCurrentMinute
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

                    if ( (intOldStandardMinutes == intNewStandardMinutes) &&
                         (intOldExtraMinutes+1 == intNewExtraMinutes) )
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
                if ( !newMinute.Contains("+") && 
                     !newMinute.Equals("HT") && 
                     !newMinute.Equals("FT") )
                {
                    // Accept next Standard minute if we already are in Standard minutes,
                    // e.g. from 57 to 58 or from 0 to 1
                    var intOldMinute = Int32.Parse(oldMinute);
                    var intNewMinute = Int32.Parse(newMinute);

                    if (intOldMinute+1 == intNewMinute)
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


        // method to be invoked by parent Bet's UpdateMatchScores
        public void UpdateScores(int newHomeClubScore, int newAwayClubScore)
        {
            if (_statusId != Status.Ongoing.Id)
            {
                throw new BettingDomainException("Match.UpdateScores: " +
                    $"Cannot update scores of a match with \"{_statusName}\" status.");
            }

            if (!OldAndNewScoresAreSuccesive(_homeClubScore, _awayClubScore,
                                                            newHomeClubScore, newAwayClubScore)) 
            {
                throw new BettingDomainException("Match.UpdateScores: " +
                    "Old and new score values are not succesive. " +
                    $"Old HomeClub Score: {_homeClubScore}, New HomeClub Score: {newHomeClubScore}, " +
                    $"Old AwayClub Score: {_awayClubScore}, New AwayClubScore: {newAwayClubScore}");
            }

            _homeClubScore = newHomeClubScore;
            _awayClubScore = newAwayClubScore;
            // Add a MatchScoresChangedDomainEvent (for the SignalR service)
            AddDomainEvent(new MatchScoresChangedDomainEvent(this.Id));

            var oldWinnerMatchResultId = _winnerResultId;
            var oldGoalsMatchResultId = _goalsResultId;

            CalculateResults();

            if (_winnerResultId != oldWinnerMatchResultId || 
                _goalsResultId != oldGoalsMatchResultId)
            {
                // Add a MatchResultsChangedDomainEvent to queue to be dispatched later
                AddDomainEvent(new MatchResultsChangedDomainEvent(this.Id));
            }
        }

        // helper method used by UpdateMatchScores
        private bool OldAndNewScoresAreSuccesive(int oldHomeClubScore, int oldAwayClubScore,
                                                              int newHomeClubScore, int newAwayClubScore)
        {
            if ( (newHomeClubScore == oldHomeClubScore+1 && newAwayClubScore == oldAwayClubScore) ||
                 (newHomeClubScore == oldHomeClubScore && newAwayClubScore == oldAwayClubScore+1) )
            {
                return true;
            }

            return false;
        }


        // method to be invoked by parent Bet's CancelMatch
        public void CancelMatch()
        {
            if (_statusId == Status.Completed.Id || _statusId == Status.Canceled.Id)
            {
                throw new BettingDomainException("Match.CancelMatch: " +
                    $"Cannot cancel a match with \"{_statusName}\" status.");
            }

            _statusId = Status.Canceled.Id;
            _statusName = Status.Canceled.Name;
            // Add a MatchCanceledDomainEvent (for the SignalR service)
            AddDomainEvent(new MatchCanceledDomainEvent(this.Id));

            // Add a MatchStatusChangedDomainEvent to queue to be dispatched later
            AddDomainEvent(new MatchStatusChangedDomainEvent(this.Id));
        }

        // method to be invoked when User requests a Bet cancelation
        public void CancelMatchByUserRequest()
        {
            if(_statusId != Status.Canceled.Id)
            {
                _statusId = Status.Canceled.Id;
                _statusName = Status.Canceled.Name;
                // No need to publish integration event for SignalR service as the Canceled Bet will be returned to the
                // client from the API and will be rendered again as Canceled
            }
        }


        private string CalculateUniqueIdForMatch()
        {
            // first create a concatenated string (64 chars) which consists from
            // Selection's ID MD5 hash (32 chars), Match's creation date (18 chars)
            // and a random string (14 chars)
            var randStr = RandomString(14);
            var selectionIdMD5Hash = CalculateMD5HashForString(_selectionId);
            var concatStr = selectionIdMD5Hash +
                            DateTime.UtcNow.ToString("yyyyMMddHHmmssffff") +
                            randStr;

            // then calculate the SHA256 hash of the concatenated string to make it "prettier"
            // we use SHA256 to "ensure" uniqueness for the produced Id
            var uniqueMatchId = CalculateSHA256HashForString(concatStr);

            // we finally return the unique Bet Id
            return uniqueMatchId;
        }

        // helper method for CalculateUniqueIdForMatch() that calculates SHA256 hash for provided string
        private static string CalculateSHA256HashForString(string theString)
        {
            string hash;

            using (System.Security.Cryptography.SHA256 sha256Hash = System.Security.Cryptography.SHA256.Create())
            {
                hash = BitConverter.ToString(
                  sha256Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(theString))
                ).Replace("-", String.Empty);
            }

            return hash;
        }


        // helper method for CalculateUniqueIdForMatch() that calculates MD5 hash for provided string
        private static string CalculateMD5HashForString(string theString)
        {
            string hash;
            using (System.Security.Cryptography.MD5 md5Hash = System.Security.Cryptography.MD5.Create())
            {
                hash = BitConverter.ToString(
                  md5Hash.ComputeHash(System.Text.Encoding.UTF8.GetBytes(theString))
                ).Replace("-", String.Empty);
            }
            return hash;
        }

        // helper method for CalculateUniqueIdForMatch() that creates a random string with provided length
        private static string RandomString(int length)
        {
            Random random = new Random();

            string chars = "abcdefghijklmnopqrstuvwxyz" +
                           "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                           "0123456789 ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


    }
}
