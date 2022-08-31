using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.TestingService.API.IntegrationEvents.Events
{
    public class BetPaymentFailedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }

        public string BetId { get; }

        public BetPaymentFailedIntegrationEvent(string gamblerId, string betId)
        {
            GamblerId = gamblerId;
            BetId = betId;
        }
    }
}
