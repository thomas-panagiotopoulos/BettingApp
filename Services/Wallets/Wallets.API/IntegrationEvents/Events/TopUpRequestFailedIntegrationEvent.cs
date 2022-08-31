using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.IntegrationEvents.Events
{
    public class TopUpRequestFailedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string RequestId { get; }
        public decimal Amount { get; }

        public TopUpRequestFailedIntegrationEvent(string gamblerId, string requestId, decimal amount)
        {
            GamblerId = gamblerId;
            RequestId = requestId;
            Amount = amount;

        }
    }
}
