using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.IntegrationEvents.Events.SignalRHub
{
    public class BetStatusChangedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetId { get; }
        public int NewStatusId { get; }
        public string NewStatusName { get; }

        public BetStatusChangedIntegrationEvent(string gamblerId, string betId, int newStatusId, string newStatusName)
        {
            GamblerId = gamblerId;
            BetId = betId;
            NewStatusId = newStatusId;
            NewStatusName = newStatusName;
        }
    }
}
