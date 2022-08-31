using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.TestingService.API.IntegrationEvents.Events
{
    public class MatchResultEnabledIntegrationEvent : IntegrationEvent
    {
        public string MatchId { get; }

        public int MatchResultId { get; }

        public MatchResultEnabledIntegrationEvent(string matchId, int matchResultId)
        {
            MatchId = matchId;
            MatchResultId = matchResultId;
        }
    }
}
