using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.IntegrationEvents.Events
{
    public class BetWinningsCreditedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetId { get; }
        public decimal Amount { get; }
        public decimal NewBalance { get; }
        public BetWinningsCreditedIntegrationEvent(string gamblerId, string betId, decimal amount, decimal newBalance)
        {
            GamblerId = gamblerId;
            BetId = betId;
            Amount = amount;
            NewBalance = newBalance;
        }
    }
}
