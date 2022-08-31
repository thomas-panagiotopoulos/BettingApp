using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.IntegrationEvents.Events
{
    public class BetRemovedDueToUnpaidCancelationIntegrationEvent : IntegrationEvent
    {
        public string BetId { get; }
        public string GamblerId { get; }

        public BetRemovedDueToUnpaidCancelationIntegrationEvent(string betId, string gamblerId)
        {
            BetId = betId;
            GamblerId = gamblerId;
        }
    }
}
