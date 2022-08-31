using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.IntegrationEvents.Events
{
    public class UserRequestedTopUpIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }

        public decimal Amount { get; }

        public string CardNumber { get; }

        public string SecurityNumber { get; }

        public string CardHolderName { get; }

        public string ExpirationDateMM { get; }

        public string ExpirationDateYY { get; }

        public int CardTypeId { get; }

        public string CardTypeName { get; }

        public string RequestId { get; }

        public UserRequestedTopUpIntegrationEvent(string gamblerId, decimal amount, string cardNumber, string securityNumber,
                                                  string cardHolderName, string expirationDateMM, string expirationDateYY, int cardTypeId, string cardTypeName,
                                                  string requestId)
        {
            GamblerId = gamblerId;
            Amount = amount;
            CardNumber = cardNumber;
            SecurityNumber = securityNumber;
            CardHolderName = cardHolderName;
            ExpirationDateMM = expirationDateMM;
            ExpirationDateYY = expirationDateYY;
            CardTypeId = cardTypeId;
            CardTypeName = cardTypeName;
            RequestId = requestId;
        }
    }
}
