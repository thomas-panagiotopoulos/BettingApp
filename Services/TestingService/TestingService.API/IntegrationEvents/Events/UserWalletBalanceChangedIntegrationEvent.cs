using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.TestingService.API.IntegrationEvents.Events
{
    public class UserWalletBalanceChangedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }

        public decimal NewBalance { get; }

        public UserWalletBalanceChangedIntegrationEvent(string gamblerId, decimal newBalance)
        {
            GamblerId = gamblerId;
            NewBalance = newBalance;
        }
    }
}
