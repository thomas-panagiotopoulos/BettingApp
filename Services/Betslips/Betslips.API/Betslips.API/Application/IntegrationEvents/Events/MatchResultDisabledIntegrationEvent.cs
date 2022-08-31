using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.IntegrationEvents.Events
{
    public class MatchResultDisabledIntegrationEvent : IntegrationEvent
    {
        public string MatchId { get; }

        public int MatchResultId { get; }

        public MatchResultDisabledIntegrationEvent(string matchId, int matchResultId)
        {
            MatchId = matchId;
            MatchResultId = matchResultId;
        }
    }
}
