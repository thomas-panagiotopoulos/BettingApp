using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.IntegrationEvents.Events
{
    public class MatchCanceledIntegrationEvent : IntegrationEvent
    {
        public string MatchId { get; }
        public MatchCanceledIntegrationEvent(string matchId)
        {
            MatchId = matchId;
        }
    }
}
