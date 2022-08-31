using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API.IntegrationEvents.Events
{
    public class BetRemovedDueToUnpaidCompletionIntegrationEvent : IntegrationEvent
    {
        public string BetId { get; }
        public string GamblerId { get; }

        public BetRemovedDueToUnpaidCompletionIntegrationEvent(string betId, string gamblerId)
        {
            BetId = betId;
            GamblerId = gamblerId;
        }
    }
}
