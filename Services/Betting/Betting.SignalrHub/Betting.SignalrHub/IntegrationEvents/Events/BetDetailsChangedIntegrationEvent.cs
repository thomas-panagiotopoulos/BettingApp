using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BettingApp.Services.Betting.SignalrHub.IntegrationEvents.Events
{
    public class BetDetailsChangedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetId { get; }
        public decimal OldTotalOdd { get; }     // only used by Notifications.API
        public decimal NewTotalOdd { get; }
        public decimal NewPotentialWinnings { get; }
        public decimal NewPotentialProfit { get; }

        public BetDetailsChangedIntegrationEvent(string gamblerId, string betId, decimal oldTotalOdd,
                                                decimal newTotalOdd, decimal newPotentialWinnings, decimal newPotentialProfit)
        {
            GamblerId = gamblerId;
            BetId = betId;
            OldTotalOdd = oldTotalOdd;
            NewTotalOdd = newTotalOdd;
            NewPotentialWinnings = newPotentialWinnings;
            NewPotentialProfit = newPotentialProfit;
        }
    }
}
