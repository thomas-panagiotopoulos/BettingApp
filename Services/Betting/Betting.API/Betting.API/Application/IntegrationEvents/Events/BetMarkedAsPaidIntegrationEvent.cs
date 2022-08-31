using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.IntegrationEvents.Events
{
    public class BetMarkedAsPaidIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }

        public string BetId { get; }

        public decimal WageredAmount { get; }

        public BetMarkedAsPaidIntegrationEvent(string gamblerId, string betId, decimal wageredAmount)
        {
            GamblerId = gamblerId;
            BetId = betId;
            WageredAmount = wageredAmount;
        }
    }
}
