using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.IntegrationEvents.Events
{
    public class WalletBalanceChangedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }

        public decimal NewBalance { get; }

        public decimal OldBalance { get; }

        public WalletBalanceChangedIntegrationEvent(string gamblerId, decimal newBalance, decimal oldBalance)
        {
            GamblerId = gamblerId;
            NewBalance = newBalance;
            OldBalance = oldBalance;
        }
    }
}
