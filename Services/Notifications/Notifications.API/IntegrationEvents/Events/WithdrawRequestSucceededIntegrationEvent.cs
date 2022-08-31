using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API.IntegrationEvents.Events
{
    public class WithdrawRequestSucceededIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string RequestId { get; }
        public decimal Amount { get; }
        public decimal NewBalance { get; }
        public WithdrawRequestSucceededIntegrationEvent(string gamblerId, string requestId, decimal amount, decimal newBalance)
        {
            GamblerId = gamblerId;
            RequestId = requestId;
            Amount = amount;
            NewBalance = newBalance;
        }
    }
}
