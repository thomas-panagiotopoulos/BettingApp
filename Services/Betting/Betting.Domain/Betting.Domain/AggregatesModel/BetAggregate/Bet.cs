using BettingApp.Services.Betting.Domain.Seedwork;
using BettingApp.Services.Betting.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MoreLinq.Extensions;
using BettingApp.Services.Betting.Domain.Events;

namespace BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate
{
    public class Bet : Entity, IAggregateRoot
    {
        // class fields and "getter" methods

        // Id

        public string GamblerId => _gamblerId;
        private string _gamblerId;


        public DateTime DateTimeCreated => _dateTimeCreated;
        private DateTime _dateTimeCreated;


        public bool IsPaid => _isPaid;
        private bool _isPaid;

        public bool IsCancelable => _isCancelable;
        private bool _isCancelable;

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

        public decimal WageredAmount => _wageredAmount;
        private decimal _wageredAmount;

        public decimal TotalOdd => _totalOdd;
        private decimal _totalOdd;
   
        public decimal PotentialWinnings => _potentialWinnings;
        private decimal _potentialWinnings;

        public decimal PotentialProfit => _potentialProfit;
        private decimal _potentialProfit;

        public decimal InitialTotalOdd => _initialTotalOdd;
        private decimal _initialTotalOdd;

        public decimal InitialPotentialWinnings => _initialPotentialWinnings;
        private decimal _initialPotentialWinnings;

        public decimal InitialPotentialProfit => _initialPotentialProfit;
        private decimal _initialPotentialProfit;


        public IReadOnlyCollection<Selection> Selections => _selections;
        private readonly List<Selection> _selections;


        // constructors

        // constructor with no arguments to be used by other constructor the fill some fields
        // with default values
        protected Bet()
        {
            _isPaid = false;
            _isCancelable = false;
            SetPendingStatus();
            _wageredAmount = 0;
            _totalOdd = 1;
            _potentialWinnings = 0;
            _potentialProfit = 0;
            _initialTotalOdd = _totalOdd;
            _initialPotentialWinnings = _potentialWinnings;
            _initialPotentialProfit = _potentialProfit;
            _selections = new List<Selection>();
        }

        // Main constructor of the class, which creates an instance of the class but
        // without any Selections. Selections have to be added afterwards by calling AddSelection method.
        // For that reason, this constructor should always be invoked along with at least one invokation
        // to AddSelection afterwards, in order to add at least one Selection to the Bet to be valid
        public Bet(string betId, string gamblerId, decimal wageredAmount) : this ()
        {
            _gamblerId = gamblerId;
            _wageredAmount = wageredAmount;
            _dateTimeCreated = DateTime.UtcNow.AddHours(2);

            // Calculate a unique betId, based on gambledId and dateTimeCreated, to pass to child
            // Selection entities 
            //Id = CalculateUniqueIdForBet();

            // We use a pre-created human-readable unique Id
            Id = betId;

            //=== Add a BetStartedDomainEvent here ===//
            AddDomainEvent(new BetStartedDomainEvent(this.Id));
        }


        // class methods implementing business logic

        public void AddSelection(int gamblerMatchResultId, decimal odd, string relatedMatchId,
                                string homeClubName, string awayClubName, DateTime kickoffDateTime,
                                string currentMinute,int homeClubScore, int awayClubScore)
        {
            if (_statusId == Status.Completed.Id || _statusId == Status.Canceled.Id)
            {
                throw new BettingDomainException("Bet.AddSelection: " +
                    $"Cannot add a selection to a Bet with \"{_statusName}\" status.");
            }

            var betId = this.Id;
            var newSelection = new Selection(betId, gamblerMatchResultId, odd, relatedMatchId, 
                                            homeClubName, awayClubName, kickoffDateTime, 
                                            currentMinute, homeClubScore, awayClubScore);
            
            _selections.Add(newSelection);

            // check if the newly added selection produced any contradictions
            if ( !CheckSelectionsForContradictions() )
                throw new BettingDomainException("Bet: Given selections contradict with each other.");

            // if newly created selection is ok, then update Bet info appropriately
            CalculateTotalOdd();
            CalculatePotentialWinningsAndProfit();
            // also update the initial values of TotalOdd, Winnings and Profit
            _initialTotalOdd = _totalOdd;
            _initialPotentialWinnings = _potentialWinnings;
            _initialPotentialProfit = _potentialProfit;


            // calculate the BetStatus after adding a Selection
            CalculateStatus();

            // calculate Cancelable status
            // (not necessary at this point as the Bet is still not paid so the Cancelable status will always be false)
            //CalculateCancelableStatus();

            // calculate the Result after adding a Selection
            CalculateResult();

        }

        private void CalculateTotalOdd()
        {
            _totalOdd = 1;
            foreach (var selection in _selections)
            {
                // Canceled selections are not taken into account when calculating TotalOdd
                if (selection.StatusId != Status.Canceled.Id) _totalOdd *= selection.Odd;
            }
            _totalOdd = Convert.ToDecimal(Math.Round(_totalOdd, 2));
        }

        private void CalculatePotentialWinningsAndProfit()
        { 
            _potentialWinnings = _totalOdd * _wageredAmount;
            _potentialProfit = _potentialWinnings - _wageredAmount;
            _potentialWinnings = Convert.ToDecimal(Math.Round(_potentialWinnings, 2));
            _potentialProfit = Convert.ToDecimal(Math.Round(_potentialProfit, 2));
        }

        private void SetPendingStatus()
        {
            _statusId = Status.Pending.Id;
            _statusName = Status.Pending.Name;
        }

        private void SetOngoingStatus()
        {
            _statusId = Status.Ongoing.Id;
            _statusName = Status.Ongoing.Name;
        }

        private void SetCompletedStatus()
        {
            _statusId = Status.Completed.Id;
            _statusName = Status.Completed.Name;
        }

        private void SetCanceledStatus()
        {
            _statusId = Status.Canceled.Id;
            _statusName = Status.Canceled.Name;
        }

        // class method that calculates the Bet Status
        private void CalculateStatus()
        {
            var pendingSelectionCount = _selections.Where(s => s.StatusId == Status.Pending.Id).Count();
            var ongoingSelectionCount = _selections.Where(s => s.StatusId == Status.Ongoing.Id).Count();
            var completedSelectionCount = _selections.Where(s => s.StatusId == Status.Completed.Id).Count();
            var canceledSelectionCount = _selections.Where(s => s.StatusId == Status.Canceled.Id).Count();
            var selectionsCount = _selections.Count();

            // Set BetStatus as Canceled when:
            // - All Selections are Canceled
            if ( canceledSelectionCount == selectionsCount )
            {
                SetCanceledStatus();
                return;
            }

            // Set BetStatus as Pending when:
            // - All Selections are Pending
            // - All Selections are Pending+Canceled
            if ( pendingSelectionCount>0  &&  
                 pendingSelectionCount + canceledSelectionCount == selectionsCount ) 
            {
                SetPendingStatus();
                return;
            }

            // Set BetStatus as Completed when:
            // - All Selections are Completed
            // - All Selections are Completed+Canceled
            if ( completedSelectionCount>0  && 
                 completedSelectionCount + canceledSelectionCount == selectionsCount )
            {
                SetCompletedStatus();
                return;
            }

            // Set BetStatus as Ongoing when:
            // - Any of the Selections is Ongoing
            // - All Selections are Pending+Completed
            // - All Selections are Pending+Completed+Canceled
            if ( (ongoingSelectionCount>0) ||               
                 (pendingSelectionCount + completedSelectionCount + canceledSelectionCount == selectionsCount  && 
                  pendingSelectionCount>0 && completedSelectionCount>0) )
            {
                SetOngoingStatus();
                return;
            }
        }

        private void SetWonResult()
        {
            _resultId = BettingResult.Won.Id;
            _resultName = BettingResult.Won.Name;
        }

        private void SetLostResult()
        {
            _resultId = BettingResult.Lost.Id;
            _resultName = BettingResult.Lost.Name;
        }


        // class method that calculates the Bet Result
        private void CalculateResult()
        {
            // if any of the non-canceled Selections has Result="Lost",
            // then the Result is "Lost" too. Otherwise the Result is "Won"
            if ( _selections.Where( s => (s.ResultId == BettingResult.Lost.Id) && 
                                         (s.StatusId != Status.Canceled.Id))
                            .Any() )
            {
                SetLostResult();
            }
            else
            {
                SetWonResult();
            }
        }

        // method that calculates if the Bet is able to be Canceled by the user
        // (it is recalculated every time Bet Status changes or IsPaid status changes)
        private void CalculateCancelableStatus()
        {
            _isCancelable = _statusId == Status.Pending.Id && _isPaid;
        }

        // method to be invoked by RecalculateBetStatusCommand handler
        public void RecalculateBetStatus()
        {
            if (_statusId == Status.Completed.Id || _statusId == Status.Canceled.Id)
            {
                throw new BettingDomainException("Bet.RecalculateBetStatus: " +
                    $"Cannot recalculate status of a bet with \"{_statusName}\" status.");
            }

            var oldStatusId = _statusId;
            CalculateStatus();
            if(_statusId != oldStatusId)
            {
                // Add a BetStatusChangedDomainEvent (for the SignalR service)
                AddDomainEvent(new BetStatusChangedDomainEvent(this.Id));
            }


            // We also calculate again Cancelable status in case it was changed after the Bet status changed
            var oldIsCancelable = _isCancelable;
            CalculateCancelableStatus();
            if(_isCancelable != oldIsCancelable)
            {
                // Add a BetCancelableStatusChangedDomainEvent (for the SignalR service)
                AddDomainEvent(new BetCancelableStatusChangedDomainEvent(this.Id));
            }

            // We calculate again Bet's Result, TotalOdd, PotentialWinnings and PotentialProfit,
            // in case any of the Selections got canceled and as a result any of these fields
            // may have changed
            var oldResultId = _resultId;
            CalculateResult();
            if (_resultId != oldResultId)
            {
                // Add a BetResultChangedDomainEvent (for the SignalR service)
                AddDomainEvent(new BetResultChangedDomainEvent(this.Id));
            }


            var oldTotalOdd = _totalOdd;
            CalculateTotalOdd();
            CalculatePotentialWinningsAndProfit();

            if (_totalOdd != oldTotalOdd)
            {
                // Add a BetDetailsChangedDomainEvent to queue to be dispatched later
                AddDomainEvent(new BetDetailsChangedDomainEvent(this.Id, oldTotalOdd));
            }

            if (_statusId == Status.Completed.Id)
            {

                if (_isPaid == false)
                {
                    // Add a BetCompletedButIsNotPaidDomainEvent to queue to be dispatched later
                    AddDomainEvent(new BetCompletedButIsNotPaidDomainEvent(this.Id));
                    
                }
                else
                {
                    // Add a BetCompletedDomainEvent to queue to be dispatched later
                    //AddDomainEvent(new BetCompletedDomainEvent(this.Id));

                    if (_resultId == BettingResult.Won.Id)
                    {
                        // Add a BetCompletedAsWonDomainEvent to queue to be dispatched later
                        AddDomainEvent(new BetCompletedAsWonDomainEvent(this.Id));
                    }
                    else if (_resultId == BettingResult.Lost.Id)
                    {
                        // Add a BetCompletedAsLostDomainEvent to queue to be dispatched later
                        AddDomainEvent(new BetCompletedAsLostDomainEvent(this.Id));
                    }

                }

            }

            if (_statusId == Status.Canceled.Id)
            {
                if (_isPaid == false)
                {
                    // Add a BetCanceledButIsNotPaidDomainEvent to queue to be dispatched later
                    AddDomainEvent(new BetCanceledButIsNotPaidDomainEvent(this.Id));

                }
                else
                {
                    // Add a BetCanceledDomainEvent to queue to be dispatched later
                    AddDomainEvent(new BetCanceledDomainEvent(this.Id));
                }
                
            }
        }


        // method to be invoked by RecalculateBetResultCommand handler
        public void RecalculateBetResult()
        {
            if (_statusId != Status.Ongoing.Id)
            {
                throw new BettingDomainException("Bet.RecalculateBetResult: " +
                    $"Cannot recalculate result of a bet with \"{_statusName}\" status.");
            }

            var oldResultId = _resultId;
            CalculateResult();
            if (_resultId != oldResultId)
            {
                // Add a BetResultChangedDomainEvent (for the SignalR service)
                AddDomainEvent(new BetResultChangedDomainEvent(this.Id));
            }
        }


        // method to be invoked by RecalculateSelectionStatusCommand handler
        public void RecalculateSelectionStatus(string selectionId)
        {
            if (_statusId == Status.Completed.Id || _statusId == Status.Canceled.Id)
            {
                throw new BettingDomainException("Bet.RecalculateSelectionStatus: " +
                    $"Cannot recalculate status of a selection, whose parent bet has \"{_statusName}\" status.");
            }

            var selectionToUpdate = Selections
                                    .Where(s => s.Id.Equals(selectionId))
                                    .FirstOrDefault();

            if (selectionToUpdate == null)
            {
                throw new BettingDomainException("Bet.RecalculateSelectionStatus: " +
                    $"Selection with id: \"{selectionId}\" was not found in the bet.");
            }

            selectionToUpdate.RecalculateStatus();

        }

        // method to be invoked by RecalculateSelectionResultCommand handler
        public void RecalculateSelectionResult(string selectionId)
        {
            if (_statusId != Status.Ongoing.Id)
            {
                throw new BettingDomainException("Bet.RecalculateSelectionResult: " +
                    $"Cannot recalculate result of a selection, whose parent bet has \"{_statusName}\" status.");
            }

            var selectionToUpdate = Selections
                                    .Where(s => s.Id.Equals(selectionId))
                                    .FirstOrDefault();

            if (selectionToUpdate == null)
            {
                throw new BettingDomainException("Bet.RecalculateSelectionResult: " +
                    $"Selection with id: \"{selectionId}\" was not found in the bet.");
            }

            selectionToUpdate.RecalculateResult();

        }


        // method to be invoked by UpdateMatchCurrentMinuteCommand handler
        public void UpdateMatchCurrentMinute(string matchId, string newMinute)
        {
            if (_statusId == Status.Completed.Id || _statusId == Status.Canceled.Id)
            {
                throw new BettingDomainException("Bet.UpdateMatchCurrentMinute: " +
                    $"Cannot update minute of a match, whose parent bet has \"{_statusName}\" status.");
            }

            var matchToUpdate = Selections
                                .Where(s => s.Match.Id.Equals(matchId))
                                .Select(s => s.Match)
                                .FirstOrDefault();

            if (matchToUpdate == null)
            {
                throw new BettingDomainException("Bet.UpdateMatchCurrentMinute: " +
                    $"Match with id: \"{matchId}\" was not found in the bet.");
  
            }

            matchToUpdate.UpdateCurrentMinute(newMinute);
   
        }


        // method to be invoked by UpdateMatchScoresCommand handler
        public void UpdateMatchScores(string matchId, int newHomeClubScore, int newAwayClubScore)
        {
            if (_statusId != Status.Ongoing.Id)
            {
                throw new BettingDomainException("Bet.UpdateMatchScores: " +
                    $"Cannot update scores of a match, whose parent bet has \"{_statusName}\" status.");
            }

            var matchToUpdate = Selections
                                .Where(s => s.Match.Id.Equals(matchId))
                                .Select(s => s.Match)
                                .FirstOrDefault();

            if (matchToUpdate == null)
            {
                throw new BettingDomainException("Bet.UpdateMatchScores: " +
                    $"Match with id: \"{matchId}\" was not found in the bet.");
            }

            matchToUpdate.UpdateScores(newHomeClubScore, newAwayClubScore);

        }


        // method to be invoked by CancelMatchCommand handler
        public void CancelMatch(string matchId)
        {
            if (_statusId == Status.Completed.Id || _statusId == Status.Canceled.Id)
            {
                throw new BettingDomainException("Bet.CancelMatch: " +
                    $"Cannot cancel a match, whose parent bet has \"{_statusName}\" status.");
            }

            var matchToCancel = Selections
                                .Where(s => s.Match.Id.Equals(matchId))
                                .Select(s => s.Match)
                                .FirstOrDefault();

            if (matchToCancel == null)
            {
                throw new BettingDomainException("Bet.CancelMatch: " +
                    $"Match with id: \"{matchId}\" was not found in the bet.");

            }

            matchToCancel.CancelMatch();
        }


        // method to be invoked by MarkBetAsPaidCommand handler
        public void MarkBetAsPaid()
        {
            if (_statusId == Status.Completed.Id || _statusId == Status.Canceled.Id)
            {
                throw new BettingDomainException("Bet.MarkBetAsPaid: " +
                    $"Cannot mark as paid a bet that has \"{_statusName}\" status.");
            }

            if (_isPaid == true)
            {
                throw new BettingDomainException("Bet.MarkBetAsPaid: " +
                    $"Cannot mark as paid a bet that is already marked as paid.");
            }

            _isPaid = true;

            // Add a BetMarkedAsPaidDomainEvent to queue to be dispatched later
            AddDomainEvent(new BetMarkedAsPaidDomainEvent(this.Id));

            // we also calcualte the Cancelable status after the Bet's IsPaid status changes, and if it has changed
            // we add a domain event that will eventually publish an integration event to the SingalR service to
            // notify the cliend for visual changes.
            var oldIsCancelable = _isCancelable;
            CalculateCancelableStatus();
            if(_isCancelable != oldIsCancelable)
            {
                // Add a BetCancelableStatusChangedDomainEvent
                AddDomainEvent(new BetCancelableStatusChangedDomainEvent(this.Id));
            }
            
        }


        // method to be invoked by CancelBetCommand handler
        public void CancelBetByUserRequest()
        {
            if (_isCancelable == false)
            {
                throw new BettingDomainException("Bet.CancelBet: " +
                    $"Cannot cancel a bet that is not cancelable.");
            }

            SetCanceledStatus();
            
            // If we want all the child Selections to be visualized as "Canceled" on the client,
            // then execute the following line. Otherwise, the client will display the Bet as canceled but the
            // Selections will appear either as Pending or as Canceled and Pending
            _selections.ForEach(s => s.CancelSelectionByUserRequest());

            // Add a BetCanceledDomainEvent to queue to be dispatched later
            AddDomainEvent(new BetCanceledDomainEvent(this.Id));

            // Also calculcate again Cancelable status, after Bet status has changed,
            // No need to publish integration event to SignalR service, as updated data will be returned by the API
            // and client will reload the page with correct visualization data.
            CalculateCancelableStatus();

        }

        // method to be invoked by CancelBetDueToRejectedPaymentCommand handler
        public void CancelBetWhenPaymentIsRejected()
        {
            if (_statusId == Status.Completed.Id || _statusId == Status.Canceled.Id)
            {
                throw new BettingDomainException("Bet.CancelBetWhenPaymentIsRejected: " +
                    $"Cannot cancel a bet that has \"{_statusName}\" status.");
            }

            // Check if Status has changed after the cancelation in order to notify the SignalR service for 
            // visual changed in the clients
            // note: checking is not really necessary, as the Bet Status will always change when Bet is getting canceled.
            var oldStatusId = _statusId;
            SetCanceledStatus();
            if (_statusId != oldStatusId)
            {
                // Add a BetStatusChangedDomainEvent (for the SignalR service)
                AddDomainEvent(new BetStatusChangedDomainEvent(this.Id));
            }

            // Add a BetCanceledDueToRejectedPaymentDomainEvent to queue to be dispatched later
            AddDomainEvent(new BetCanceledDueToRejectedPaymentDomainEvent(this.Id));

            // No need to calculate again Cancelable status, as it will be already false for sure since the Bet
            // has not been paid yet.
            //CalculateCancelableStatus();
        }


        // class method that checks the given selections for contradictions, based on business rules
        // e.g. one Bet can't have 2 selections with same Related Match and same Selection Type
        // we are using MoreLinq package to have DistinctBy (Enumerable method) available in this method
        private bool CheckSelectionsForContradictions()
        {
            var distinctCount = _selections.DistinctBy(s => new { s.Match.RelatedMatchId, s.SelectionTypeId }).Count();

            var totalCount = _selections.Count();

            if (distinctCount == totalCount) return true;

            return false;
        }


        // class method that claculates a unique Id for each Bet in order to link this Bet with
        // its child Selection/Match objects, to avoid multiple DomainEvents handlings that are raised
        // by Match objects
        private string CalculateUniqueIdForBet()
        {
            // first create a concatenated string (64 chars) which consists from Gambler's ID (32 chars),
            // Bet's creation date (18 chars) and a random string (14 chars)
            var randStr = RandomString(14);
            var concatStr = _gamblerId.Replace("-", String.Empty) + 
                            _dateTimeCreated.ToString("yyyyMMddHHmmssffff") + 
                            randStr;

            // then calculate the SHA256 hash of the concatenated string to make it "prettier"
            // we use SHA256 to "ensure" uniqueness for the produced Id
            var uniqueBetId = CalculateSHA256HashForString(concatStr);

            // we finally return the unique Bet Id
            return uniqueBetId;
        }

        // helper method for CalculateUniqueIdForBet() that calculates SHA256 hash for provided string
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

        // helper method for CalculateUniqueIdForBet() that creates a random string with provided length
        private static string RandomString(int length)
        {
            Random random = new Random();

            string chars = "abcdefghijklmnopqrstuvwxyz"+
                           "ABCDEFGHIJKLMNOPQRSTUVWXYZ"+
                           "0123456789 ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
