using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API.IntegrationEvents.Events
{
    public class BetCompletedAsWonIntegrationEvent : IntegrationEvent
    {
        public string BetId { get; }

        public string GamblerId { get; }

        public decimal TotalWinnings { get; }


        public BetCompletedAsWonIntegrationEvent(string betId, string gamblerId, decimal totalWinnings)
        {
            BetId = betId;
            GamblerId = gamblerId;
            TotalWinnings = totalWinnings;
        }
    }
}
