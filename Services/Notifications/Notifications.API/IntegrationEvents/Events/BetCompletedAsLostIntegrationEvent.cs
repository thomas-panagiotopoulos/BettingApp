using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API.IntegrationEvents.Events
{
    public class BetCompletedAsLostIntegrationEvent : IntegrationEvent
    {
        public string BetId { get; }

        public string GamblerId { get; }


        public BetCompletedAsLostIntegrationEvent(string betId, string gamblerId)
        {
            BetId = betId;
            GamblerId = gamblerId;
        }
    }
}
