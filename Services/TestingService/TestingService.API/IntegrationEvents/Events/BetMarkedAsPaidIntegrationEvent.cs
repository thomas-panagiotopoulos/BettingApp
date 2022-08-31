using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.TestingService.API.IntegrationEvents.Events
{
    public class BetMarkedAsPaidIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }

        public BetMarkedAsPaidIntegrationEvent(string gamblerId)
        {
            GamblerId = gamblerId;
        }
    }
}
