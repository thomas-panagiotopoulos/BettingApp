using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// note: this IntegrationEvent also exists in the "traditional" folder of IntegrationEvents, but has also a duplicate
// copy here. This is because this specific IE notifies both the SignalR service for visual changes on clients, but also
// notifies the Notifications service to create a notification for the user when the TotalOdd of a Bet changes.

namespace BettingApp.Services.Betting.API.Application.IntegrationEvents.Events.SignalRHub
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
