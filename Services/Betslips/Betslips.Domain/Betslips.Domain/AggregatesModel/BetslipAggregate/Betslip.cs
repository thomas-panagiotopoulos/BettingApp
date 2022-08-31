using BettingApp.Services.Betslips.Domain.Events;
using BettingApp.Services.Betslips.Domain.Exceptions;
using BettingApp.Services.Betslips.Domain.Seedwork;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate
{
    public class Betslip : Entity, IAggregateRoot
    {
        // Id

        public string GamblerId => _gamblerId;
        private string _gamblerId;

        public decimal WageredAmount => _wageredAmount;
        private decimal _wageredAmount;
        public const decimal MaxWageredAmountLimit = 5000.00m;
        public decimal TotalOdd => _totalOdd;
        private decimal _totalOdd;


        public decimal PotentialWinnings => _potentialWinnings;
        private decimal _potentialWinnings;


        public decimal PotentialProfit => _potentialProfit;
        private decimal _potentialProfit;

        public bool IsBetable => _isBetable;
        private bool _isBetable;

        public int MaxSelectionsLimit => _maxSelectionsLimit;
        private const int _maxSelectionsLimit = 30;

        public string LatestAdditionId => _latestAdditionId;
        private string _latestAdditionId;
        public IReadOnlyCollection<Selection> Selections => _selections;
        private readonly List<Selection> _selections;

        // constructors

        protected Betslip()
        {
            _isBetable = false;
            _selections = new List<Selection>();

            _wageredAmount = 0;
            CalculateTotalOdd();
            CalculateWinningsAndProfit();

            _latestAdditionId = String.Empty;
        }

        public Betslip(string gamblerId) : this()
        {
            _gamblerId = gamblerId;
            Id = CalculateUniqueIdForBetslip();

        }


        // class methods implementing business logic

        // method to be used by AddSelectionToBetslipCommand
        public void AddSelection(int gamblerMatchResultId, decimal odd, decimal initialOdd, string relatedMatchId,
                                string homeClubName, string awayClubName, DateTime kickoffDateTime,
                                string currentMinute, int homeClubScore, int awayClubScore,
                                int ruleTypeId, decimal ruleValue, string latestAdditionId)
        {
            var betslipId = this.Id;
            var newSelection = new Selection(betslipId, gamblerMatchResultId, odd, initialOdd, relatedMatchId,
                                            homeClubName, awayClubName, kickoffDateTime,
                                            currentMinute, homeClubScore, awayClubScore,
                                            ruleTypeId, ruleValue);

            // After creating Selection, check explicitly if child Match is betable, and deny addition if it's not
            // (if child Match is not betable, that means it's either in final minutes or canceled, so there's
            // no way it will become betable in the future).
            if(!newSelection.Match.IsBetable)
            {
                throw new BetslipsDomainException("Betslip.AddSelection: Selection cannot be added because " +
                    "given Match is not betable (either in final minutes or canceled).");
            }

            _selections.Add(newSelection);

            // check if maximum selections limit has been reached
            if(_selections.Count > _maxSelectionsLimit)
            {
                _selections.Remove(newSelection); // not necessary as thrown exception will reverse any change on model
                throw new BetslipsDomainException("Betslip.AddSelection: Selection cannot be added because " +
                    "maximum selections limit has been reached.");
            }

            // check if the newly added selection produced any contradictions
            if (SelectionsContradictWithEachOther())
            {
                _selections.Remove(newSelection); // not necessary as thrown exception will reverse any change on model
                throw new BetslipsDomainException("Betslip.AddSelection: Selection to add contradict " +
                    "with the current selections.");
            }
                

            CalculateTotalOdd();
            CalculateWinningsAndProfit();

            // Recalculate fulfillment of all Selections' Requirements, because Selections.Count changed.
            // With this calculation, the newly added Selection Requirment's fulfillment will also
            // be calculated
            CalculateRequirementsFulfillment();

            CalculateBetableStatus();

            // update LatestAdditionId (used to ensure eventual consistency when adding a selection)
            _latestAdditionId = latestAdditionId;
        }

        // method to be used by RemoveSelectionFromBetslipCommand
        public void RemoveSelection(string selectionId) // remove by Id or by RelatedMatchId and SelectionType
        {
            var selectionToRemove = _selections.FirstOrDefault(s => s.Id.Equals(selectionId));

            if(selectionToRemove == null)
            {
                throw new BetslipsDomainException("Betslip.RemoveSelection: Selection to remove " +
                                                  "was not found in the Betslip.");
            }

            _selections.Remove(selectionToRemove);

            CalculateTotalOdd();
            CalculateWinningsAndProfit();

            // Recalculate fulfillment status of all Selections' Requirements, because Selections.Count changed
            CalculateRequirementsFulfillment();

            CalculateBetableStatus();
        }


        // method to be invoked by RecalculateBetslipTotalOddCommand
        // (can be initiated by MatchResultUpdated)
        public void RecalculateBetslipTotalOdd()
        {
            var oldTotalOdd = _totalOdd;
            var oldPotentialWinnings = _potentialWinnings;
            var oldPotentialProfit = _potentialProfit;

            CalculateTotalOdd();
            CalculateWinningsAndProfit();

            if(_totalOdd != oldTotalOdd || _potentialWinnings != oldPotentialWinnings || _potentialProfit != oldPotentialProfit)
            {
                // Add a BetslipDetailsChangedDomainEvent
                // (in order to send a BetslipDetailsChanged event through the SignalR service)
                AddDomainEvent(new BetslipDetailsChangedDomainEvent(this.Id));
            }
        }


        // method to be invoked by RecalculateBetslipBetableStatusCommand
        // (can be initiated by MatchCanceled, MatchMinuteChanged, AddSelection, RemoveSelection, UpdateWageredAmount,
        // MatchResultUpdated)
        public void RecalculateBetslipBetableStatus()
        {
            var oldBetableStatus = _isBetable;
            CalculateBetableStatus();

            if(_isBetable != oldBetableStatus)
            {
                // Add a BetslipBetableStatusChangedDomainEvent
                // (in order to send a BetslipBetableStatusChanged event through the SignalR service)
                AddDomainEvent(new BetslipBetableStatusChangedDomainEvent(this.Id));
            }
        }

        // method to be invoked by ReclaculateSelectionBetableStatusCommand
        public void ReclaculateSelectionBetableStatus(string selectionId)
        {
            var selectionToUpdate = _selections.FirstOrDefault(s => s.Id.Equals(selectionId));

            if(selectionToUpdate == null)
            {
                throw new BetslipsDomainException("Betslip.ReclaculateSelectionBetableStatus: Selection to " +
                                                  "recalculate betable status was not found in the Betslip.");
            }

            selectionToUpdate.RecalculateSelectionBetableStatus();
        }


        // method to be invoked by UpdateMatchCurrentMinuteCommand
        public void UpdateMatchCurrentMinute(string matchId, string newMinute)
        {
            var matchToUpdate = _selections.Select(s => s.Match)
                                           .FirstOrDefault(m => m.Id.Equals(matchId));

            if (matchToUpdate == null)
            {
                throw new BetslipsDomainException("Betslip.UpdateMatchCurrentMinite: Match to " +
                                                  "update was not found in the Betslip.");
            }

            matchToUpdate.UpdateCurrentMinute(newMinute);
        }


        // method to be invoked by UpdateMatchScoresCommand
        public void UpdateMatchScores(string matchId, int newHomeClubScore, int newAwayClubScore)
        {
            var matchToUpdate = _selections.Select(s => s.Match)
                                           .FirstOrDefault(m => m.Id.Equals(matchId));

            if (matchToUpdate == null)
            {
                throw new BetslipsDomainException("Betslip.UpdateMatchScores: Match to " +
                                                  "update was not found in the Betslip.");
            }

            matchToUpdate.UpdateScores(newHomeClubScore, newAwayClubScore);
        }

        
        // method to be invoked by CancelMatchCommand
        public void CancelMatch(string matchId)
        {
            var matchToCancel = _selections.Select(s => s.Match)
                                           .FirstOrDefault(m => m.Id.Equals(matchId));

            if (matchToCancel == null)
            {
                throw new BetslipsDomainException("Betslip.CancelMatch: Match to " +
                                                  "cancel was not found in the Betslip.");
            }

            matchToCancel.CancelMatch();
        }


        // method to be invoked by UpdateSelectionOddCommand
        // (not used anymore, UpdateSelection method is used in its place)
        public void UpdateSelectionOdd(string selectionId, decimal newOdd)
        {
            var selectionToUpdate = _selections.FirstOrDefault(s => s.Id.Equals(selectionId));

            if (selectionToUpdate == null) 
            {
                throw new BetslipsDomainException("Betslip.UpdateSelectionOdd: Selection to update " +
                                                  "was not found in the Betslip.");
            }

            selectionToUpdate.UpdateOdd(newOdd);
        }

        // method to be invoked by EnableSelectionCommand
        // (not used anymore, UpdateSelection method is used in its place)
        public void DisableSelection(string selectionId)
        {
            var selectionToUpdate = _selections.FirstOrDefault(s => s.Id.Equals(selectionId));

            if (selectionToUpdate == null)
            {
                throw new BetslipsDomainException("Betslip.DisableSelection: Selection to update " +
                                                  "was not found in the Betslip.");
            }

            selectionToUpdate.Disable();
        }

        // method to be invoked by DisableSelectionCommand 
        // (not used anymore, UpdateSelection method is used in its place)
        public void EnableSelection(string selectionId)
        {
            var selectionToUpdate = _selections.FirstOrDefault(s => s.Id.Equals(selectionId));

            if (selectionToUpdate == null)
            {
                throw new BetslipsDomainException("Betslip.EnableSelection: Selection to update " +
                                                  "was not found in the Betslip.");
            }

            selectionToUpdate.Enable();
        }

        // method to be invoked by UpdateSelectionCommand
        public void UpdateSelection(string selectionId, decimal newOdd, bool isDisabled)
        {
            var selectionToUpdate = _selections.FirstOrDefault(s => s.Id.Equals(selectionId));

            if (selectionToUpdate == null)
            {
                throw new BetslipsDomainException("Betslip.UpdateSelection: Selection to update " +
                                                  "was not found in the Betslip.");
            }

            selectionToUpdate.Update(newOdd, isDisabled);
        }

        // method to be used by UpdateWageredAmountCommand
        public void UpdateWageredAmount(decimal newAmount)
        {
            if (newAmount > MaxWageredAmountLimit)
            {
                throw new BetslipsDomainException("Betslip.UpdateWageredAmount: Wagered amount cannot exceed " +
                    $"maximum value ({MaxWageredAmountLimit} €)");
            }

            _wageredAmount = newAmount;
            CalculateWinningsAndProfit();

            // Recalculate fulfillment status of all Selections' Requirements, because WageredAmount changed
            CalculateRequirementsFulfillment(); 

            CalculateBetableStatus();
        }

        // method to be used by API controller (CheckSelectionAddition)
        public bool CanAddSelection(string matchId, int matchResultId)
        {
            // cannot add any Selection if maximu selections limit has been reached
            if (Selections.Count >= _maxSelectionsLimit)
                return false;

            // cannot add a Selection if Betslip already contains another Selection
            // with the same Match and the same SelectionType
            if (Selections.Any(s => s.Match.RelatedMatchId.Equals(matchId) &&
                                    s.SelectionTypeId == MatchResult.From(matchResultId).TypeId))
                return false;
            
            // otherwise, the Selection can be added
            return true;
        }

        // method to be used by API controller (VerifyLatestAdditionId)
        public bool LatestAdditionIdMatches(string latestAdditionId)
        {
            return _latestAdditionId.Equals(latestAdditionId);
        }

        private void CalculateRequirementsFulfillment()
        {
            _selections.ForEach(s => s.Requirement.CalculateFulfillment(parentBetslip: this));
        }

        private void CalculateTotalOdd()
        {
            _totalOdd = 1;
            _selections.ForEach(s => _totalOdd *= s.Odd);
            _totalOdd = Convert.ToDecimal(Math.Round(_totalOdd, 2));
        }

        private void CalculateWinningsAndProfit()
        {
            _potentialWinnings = _totalOdd * _wageredAmount;
            _potentialProfit = _potentialWinnings - _wageredAmount;
            _potentialWinnings = Convert.ToDecimal(Math.Round(_potentialWinnings, 2));
            _potentialProfit = Convert.ToDecimal(Math.Round(_potentialProfit, 2));
        }

        private void CalculateBetableStatus()
        {
            // Conditions for a Betslip to be betable:
            // - Wagered amount must be more than 0
            // - have at least one Selection
            // - all Selections must be betable

            _isBetable = (_wageredAmount > 0) && _selections.Any() && _selections.All(s => s.IsBetable == true);
        }

        // class method that checks the given selections for contradictions, based on business rules
        // e.g. a Betslip can't have 2 selections with same Related Match and same Selection Type
        // we are using MoreLinq package to have DistinctBy (linq method) available in this method
        private bool SelectionsContradictWithEachOther()
        {
            var distinctCount = _selections.DistinctBy(s => new { s.Match.RelatedMatchId, s.SelectionTypeId }).Count();

            var totalCount = _selections.Count();

            if (distinctCount == totalCount) return false; // selections are clear of contradictions

            return true; // selections have contraditictions
        }


        // class method that claculates a unique Id for the Betslip, based on the gambler's Id
        // and Betslip's creation datetime
        private string CalculateUniqueIdForBetslip()
        {
            // first create a concatenated string (64 chars) which consists from Gambler's ID (32 chars),
            // Bet's creation date (18 chars) and a random string (14 chars)
            var randStr = RandomString(14);
            var datetime = DateTime.UtcNow;
            var concatStr = _gamblerId.Replace("-", String.Empty) +
                            datetime.ToString("yyyyMMddHHmmssffff") +
                            randStr;

            // then calculate the SHA256 hash of the concatenated string to make it "prettier"
            // we use SHA256 to "ensure" uniqueness for the produced Id
            var uniqueBetslipId = CalculateSHA256HashForString(concatStr);

            // we finally return the unique Betslip Id
            return uniqueBetslipId;
        }

        // helper method for CalculateUniqueIdForBetslip() that calculates SHA256 hash for provided string
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

        // helper method for CalculateUniqueIdForBetslip() that creates a random string with provided length
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
