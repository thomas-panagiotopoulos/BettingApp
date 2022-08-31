using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.IntegrationEvents.Events
{
    public class WelcomeBonusCreditedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public decimal WelcomeBonusAmount { get; }
        public decimal NewBalance { get; }
        public WelcomeBonusCreditedIntegrationEvent(string gamblerId, decimal welcomeBonusAmount, decimal newBalance)
        {
            GamblerId = gamblerId;
            WelcomeBonusAmount = welcomeBonusAmount;
            NewBalance = newBalance;
        }
    }
}
