using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.IntegrationEvents.Events
{
    public class BetCanceledIntegrationEvent : IntegrationEvent
    {
        public string BetId { get; }

        public string GamblerId { get; }

        public decimal WageredAmount { get; }

        public BetCanceledIntegrationEvent(string betId, string gamblerId, decimal wageredAmount)
        {
            BetId = betId;
            GamblerId = gamblerId;
            WageredAmount = wageredAmount;
        }
    }
}
