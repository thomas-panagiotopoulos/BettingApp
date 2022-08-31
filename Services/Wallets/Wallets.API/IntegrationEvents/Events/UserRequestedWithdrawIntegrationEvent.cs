using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.IntegrationEvents.Events
{
    public class UserRequestedWithdrawIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }

        public decimal Amount { get; }

        public string IBAN { get; }

        public string RequestId { get; }

        public UserRequestedWithdrawIntegrationEvent(string gamblerId, decimal amount, string iban, string requestId)
        {
            GamblerId = gamblerId;
            Amount = amount;
            IBAN = iban;
            RequestId = requestId;
        }
    }
}
