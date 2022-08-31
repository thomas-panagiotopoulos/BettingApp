using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API.IntegrationEvents.Events
{
    public class BetWageredAmountRefundedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetId { get; }
        public decimal WageredAmount { get; }
        public decimal NewBalance { get; }
        public BetWageredAmountRefundedIntegrationEvent(string gamblerId, string betId, decimal wageredAmount, decimal newBalance)
        {
            GamblerId = gamblerId;
            BetId = betId;
            WageredAmount = wageredAmount;
            NewBalance = newBalance;
        }
    }
}
