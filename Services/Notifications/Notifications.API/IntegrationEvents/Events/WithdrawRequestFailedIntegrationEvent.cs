using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API.IntegrationEvents.Events
{
    public class WithdrawRequestFailedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string RequestId { get; }
        public decimal Amount { get; }

        public WithdrawRequestFailedIntegrationEvent(string gamblerId, string requestId, decimal amount)
        {
            GamblerId = gamblerId;
            RequestId = requestId;
            Amount = amount;
        }
    }
}
