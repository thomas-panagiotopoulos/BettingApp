using BettingApp.Services.Betslips.Domain.Events;
using BettingApp.Services.Betslips.Domain.Exceptions;
using BettingApp.Services.Betslips.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate
{
    public class Selection : Entity
    {
        // Id

        public string BetslipId => _betslipId;
        private string _betslipId;

        public decimal Odd => _odd;
        private decimal _odd;

        public decimal InitialOdd => _initialOdd;
        private decimal _initialOdd;

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

        public bool IsCanceled => _isCanceled;
        private bool _isCanceled;

        public bool IsDisabled => _isDisabled;
        private bool _isDisabled;

        public bool IsBetable => _isBetable;
        private bool _isBetable;

        public Match Match => _match;
        private Match _match;

        public Requirement Requirement => _requirement;
        private Requirement _requirement;

        // constructors

        protected Selection()
        {
            _isBetable = false;
            _isDisabled = false;
        }

        public Selection(string betslipId, int gamblerMatchResultId, decimal odd, decimal initialOdd, string relatedMatchId,
                        string homeClubName, string awayClubName, DateTime kickoffDateTime,
                        string currentMinute, int homeClubScore, int awayClubScore,
                        int ruleTypeId, decimal ruleValue)
            : this()
        {
            _betslipId = betslipId;
            Id = CalculateUniqueIdForSelection();

            _gamblerMatchResultId = gamblerMatchResultId;
            _gamblerMatchResultName = MatchResult.From(gamblerMatchResultId).Name;
            _selectionTypeId = MatchResult.From(gamblerMatchResultId).TypeId;
            _selectionTypeName = MatchResult.From(gamblerMatchResultId).TypeName;

            _odd = odd;
            _initialOdd = initialOdd;

            var selectionId = this.Id;
            _match = new Match(selectionId, relatedMatchId, homeClubName, awayClubName, kickoffDateTime,
                               currentMinute, homeClubScore, awayClubScore);

            _requirement = new Requirement(selectionId, relatedMatchId, _selectionTypeId, 
                                            ruleTypeId, ruleValue);

            CalculateBetableAndCanceledStatus();
        }

        // class methods

        // class method to be invoked by parent Betslip's UpdateSelectionOdd method
        public void UpdateOdd(decimal newOdd)
        {
            if (_isCanceled)
            {
                throw new BetslipsDomainException("Selection.UpdateOdd: Cannot update odd of a selection " +
                                                    "for a canceled match.");
            }

            var oldOdd = _odd;
            _odd = newOdd;
            if(_odd != oldOdd)
            {
                // Add a SelectionOddChangedDomainEvent to queue to be dispatched later
                AddDomainEvent(new SelectionOddChangedDomainEvent(this.Id));
            }
        }

        // method to be invoked by parent Betslip's DisableSelection method
        public void Disable()
        {
            if (_isCanceled)
            {
                throw new BetslipsDomainException("Selection.Disable: Cannot disable a Selection of a canceled Match.");
            }

            _isDisabled = true;

            var isBetableOld = _isBetable;
            CalculateBetableAndCanceledStatus();
            if (_isBetable != isBetableOld)
            {
                // Add a SelectionBetableStatusChangedDomainEvent to queue to be dispatched later
                AddDomainEvent(new SelectionBetableStatusChangedDomainEvent(this.Id));
            }

        }

        // method to be invoked by parent Betslip's EnableSelection method
        public void Enable()
        {
            if (_isCanceled)
            {
                throw new BetslipsDomainException("Selection.Enable: Cannot enable a Selection of a canceled Match.");
            }

            _isDisabled = false;

            var isBetableOld = _isBetable;
            CalculateBetableAndCanceledStatus();
            if (_isBetable != isBetableOld)
            {
                // Add a SelectionBetableStatusChangedDomainEvent to queue to be dispatched later
                AddDomainEvent(new SelectionBetableStatusChangedDomainEvent(this.Id));
            }
        }

        // method to be invoked by parent Betslip's UpdateSelection method (updates both Odd value and IsDisabled status)
        // (initiated by MatchResultOddOrStatusChangedIntegrationEvent)
        public void Update(decimal newOdd, bool newIsDisabled)
        {
            if (_isCanceled)
            {
                throw new BetslipsDomainException("Selection.Update: Cannot update a Selection of a canceled Match.");
            }

            var oldOdd = _odd;
            _odd = newOdd;
            if (_odd != oldOdd)
            {
                // Add a SelectionOddChangedDomainEvent to queue to be dispatched later
                AddDomainEvent(new SelectionOddChangedDomainEvent(this.Id));
            }

            _isDisabled = newIsDisabled;
            var isBetableOld = _isBetable;
            CalculateBetableAndCanceledStatus();
            if (_isBetable != isBetableOld)
            {
                // Add a SelectionBetableStatusChangedDomainEvent to queue to be dispatched later
                AddDomainEvent(new SelectionBetableStatusChangedDomainEvent(this.Id));
            }

            if (_odd != oldOdd || _isBetable != isBetableOld)
            {
                // Add a SelectionOddOrBetableStatusChangedDomainEvent to queue to be published later
                // (in order to send a SelectionOddOrBetableStatusChanged event through the SignalR hub service)
                AddDomainEvent(new SelectionOddOrBetableStatusChangedDomainEvent(this.Id));
            }
        }

        // method to be invoked by parent Betslip's RecalculateSelectionBetableStatus method
        // (initiated by MatchBetableStatusChanged or RequirementFulfillmentStatusChanged domain events)
        public void RecalculateSelectionBetableStatus()
        {
            if (_isCanceled)
            {
                throw new BetslipsDomainException("Selection.RecalculateSelectionBetableStatus: Cannot " +
                                                  "recalculate betable status for a canceled match.");
            }

            var isBetableOld = _isBetable;
            CalculateBetableAndCanceledStatus();
            if (_isBetable != isBetableOld)
            {
                // Add a SelectionBetableStatusChangedDomainEvent to queue to be dispatched later
                AddDomainEvent(new SelectionBetableStatusChangedDomainEvent(this.Id));
            }
        }

        private void CalculateBetableAndCanceledStatus()
        {
            _isBetable = _match.IsBetable && _requirement.IsFulfilled && !_isDisabled;
            _isCanceled = _match.IsCanceled;
        }


        // class method that claculates a unique Id for the Selection, based on the Betslip's Id
        // and Selections's creation datetime
        private string CalculateUniqueIdForSelection()
        {
            // first create a concatenated string (64 chars) which consists from
            // Betslips's ID MD5 hash (32 chars), Selection's creation date (18 chars)
            // and a random string (14 chars)
            var randStr = RandomString(14);
            var datetime = DateTime.UtcNow;
            var betIdMD5Hash = CalculateMD5HashForString(_betslipId);
            var concatStr = betIdMD5Hash +
                            datetime.ToString("yyyyMMddHHmmssffff") +
                            randStr;

            // then calculate the SHA256 hash of the concatenated string to make it "prettier"
            // we use SHA256 to "ensure" uniqueness for the produced Id
            var uniqueSelectionId = CalculateSHA256HashForString(concatStr);

            // we finally return the unique Selection Id
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
