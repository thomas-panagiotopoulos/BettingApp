using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.IntegrationEvents.Events
{
    public class BetPaymentSucceededIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }

        public string BetId { get; }

        public BetPaymentSucceededIntegrationEvent(string gamblerId, string betId)
        {
            GamblerId = gamblerId;
            BetId = betId;
        }

    }
}
