using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Payments.API.IntegrationEvents.Events
{
    public class ApplyWithdrawFailedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }

        public decimal Amount { get; }

        public string RequestId { get; }

        public ApplyWithdrawFailedIntegrationEvent(string gamblerId, decimal amount, string requestId)
        {
            GamblerId = gamblerId;
            Amount = amount;
            RequestId = requestId;
        }
    }
}
