using BettingApp.Services.Betting.Domain.Events;
using BettingApp.Services.Betting.Domain.Seedwork;
using BettingApp.Services.Betting.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate
{
    public class Selection : Entity
    {
        // class fields and properties
        
        //Id
    
        public string BetId => _betId;
        private string _betId;


        public Status Status { get; private set; }
        public int StatusId => _statusId;
        private int _statusId;
        public string StatusName => _statusName;
        private string _statusName;


        public BettingResult Result { get; private set; }
        public int ResultId => _resultId;
        private int _resultId;
        public string ResultName => _resultName;
        private string _resultName;


        // GamblerMatchResult includes both selected match result by gambler and selection's type
        public MatchResult GamblerMatchResult { get; private set; }
        public int GamblerMatchResultId => _gamblerMatchResultId;
        private int _gamblerMatchResultId;
        public string GamblerMatchResultName => _gamblerMatchResultName;
        private string _gamblerMatchResultName;
        
        public int SelectionTypeId => _selectionTypeId;
        private int _selectionTypeId;
        public string SelectionTypeName => _selectionTypeName;
        private string _selectionTypeName;


        public decimal Odd => _odd;
        private decimal _odd;


        public Match Match => _match;
        private Match _match;


        // constructors

        public Selection() { }
        

        public Selection(string betId, int gamblerMatchResultId, decimal odd, string relatedMatchId, 
                        string homeClubName, string awayClubName, DateTime kickoffDateTime, 
                        string currentMinute, int homeClubScore, int awayClubScore)
        {
            _betId = betId;
            Id = CalculateUniqueIdForSelection();

            _gamblerMatchResultId = gamblerMatchResultId;
            _gamblerMatchResultName = MatchResult.From(gamblerMatchResultId).Name;
            _selectionTypeId = MatchResult.From(gamblerMatchResultId).TypeId;
            _selectionTypeName = MatchResult.From(gamblerMatchResultId).TypeName;

            _odd = odd;

            var selectionId = this.Id;
            _match = new Match(selectionId, relatedMatchId, homeClubName, awayClubName, kickoffDateTime,
                                currentMinute, homeClubScore, awayClubScore);

            // Calculate the SelectionStatus here, simply by taking directly from child Match entity
            CalculateStatus();

            // Caclucate the Result here based on Match Results and the Selection Type
            CalculateResult();
        }


        // class methods implementing business logic
        
        private void CalculateStatus()
        {
            _statusId = _match.StatusId;
            _statusName = Status.From(_statusId).Name;
        }


        private void CalculateResult()
        {
            if (_selectionTypeId == MatchResult.WinnerHomeClub.TypeId) // could use WinnerDraw or WinnerAwayClub as well
            {
                // if SelectionType="Winner" then check the WinnerResult of the Match
                if (_gamblerMatchResultId == _match.WinnerResultId)
                {
                    _resultId = BettingResult.Won.Id;
                    _resultName = BettingResult.Won.Name;
                }
                else
                {
                    _resultId = BettingResult.Lost.Id;
                    _resultName = BettingResult.Lost.Name;
                }
            }
            else if (_selectionTypeId == MatchResult.GoalsUnder.TypeId) // could use GoalsOver as well
            {
                // if SelectionType="Goals" then check the GoalsResult of the Match
                if (_gamblerMatchResultId == _match.GoalsResultId)
                {
                    _resultId = BettingResult.Won.Id;
                    _resultName = BettingResult.Won.Name;
                }
                else
                {
                    _resultId = BettingResult.Lost.Id;
                    _resultName = BettingResult.Lost.Name;
                }
            }
        }


        // method to be invoked by parent Bet's RecalculateSelectionStatus
        public void RecalculateStatus()
        {
            if (_statusId == Status.Completed.Id || _statusId == Status.Canceled.Id)
            {
                throw new BettingDomainException("Selection.UpdateStatus: " +
                    $"Cannot recalculate status of a selection with \"{_statusName}\" status.");
            }

            var oldStatusId = _statusId;

            CalculateStatus();

            if (oldStatusId != _statusId)
            {
                // Add a SelectionStatusChangedDomainEvent to queue to be dispatched later
                // (this DomainEvent is also used to notify the SignalR service)
                AddDomainEvent(new SelectionStatusChangedDomainEvent(this.Id));
            }
        }


        // method to be invoked by parent Bet's RecalculateSelectionResult
        public void RecalculateResult()
        {
            if (_statusId == Status.Completed.Id || _statusId == Status.Canceled.Id)
            {
                throw new BettingDomainException("Selection.UpdateResult: " +
                    $"Cannot recalculate result of a selection with \"{_statusName}\" status.");
            }

            var oldResultId = _resultId;

            CalculateResult();

            if (oldResultId != _resultId)
            {
                // Add a SelectionResultChangedDomainEvent to queue to be dispatched later
                // (this DomainEvent is also used to notify the SignalR service)
                AddDomainEvent(new SelectionResultChangedDomainEvent(this.Id));
            }

        }

        // method to be invoked when User requests a Bet cancelation
        public void CancelSelectionByUserRequest()
        {
            if (_statusId != Status.Canceled.Id)
            {
                _statusId = Status.Canceled.Id;
                _statusName = Status.Canceled.Name;
                _match.CancelMatchByUserRequest();
            }
        }


        private string CalculateUniqueIdForSelection()
        {
            // first create a concatenated string (64 chars) which consists from
            // Bet's ID MD5 hash (32 chars), Selection's creation date (18 chars)
            // and a random string (14 chars)
            var randStr = RandomString(14);
            var betIdMD5Hash = CalculateMD5HashForString(_betId);
            var concatStr = betIdMD5Hash +
                            DateTime.UtcNow.ToString("yyyyMMddHHmmssffff") +
                            randStr;

            // then calculate the SHA256 hash of the concatenated string to make it "prettier"
            // we use SHA256 to "ensure" uniqueness for the produced Id
            var uniqueSelectionId = CalculateSHA256HashForString(concatStr);

            // we finally return the unique Bet Id
            return uniqueSelectionId;
        }

        // helper method for CalculateUniqueIdForSelection() that calculates SHA256 hash for provided string
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

        
        // helper method for CalculateUniqueIdForSelection() that calculates MD5 hash for provided string
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

        // helper method for CalculateUniqueIdForSelection() that creates a random string with provided length
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
