using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.IntegrationEvents.Events.SignalRHub
{
    public class BetResultChangedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetId { get; }
        public int NewResultId { get; }
        public string NewResultName { get; }

        public BetResultChangedIntegrationEvent(string gamblerId, string betId, int newResultId, string newResultName)
        {
            GamblerId = gamblerId;
            BetId = betId;
            NewResultId = newResultId;
            NewResultName = newResultName;
        }
    }
}
