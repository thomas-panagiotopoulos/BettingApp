using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.IntegrationEvents.Events
{
    public class UserRegisteredWithWelcomeBonusIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }

        public decimal WelcomeBonusAmount { get; }

        public UserRegisteredWithWelcomeBonusIntegrationEvent(string gamblerId, decimal welcomeBonusAmount)
        {
            GamblerId = gamblerId;
            WelcomeBonusAmount = welcomeBonusAmount;
        }
    }
}
