using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.IntegrationEvents.Events
{
    public class MatchResultOddChangedIntegrationEvent : IntegrationEvent
    {
        public string MatchId { get; }

        public int MatchResultId { get; }

        public decimal NewOdd { get; }

        public MatchResultOddChangedIntegrationEvent(string matchId, int matchResultId, decimal newOdd)
        {
            MatchId = matchId;
            MatchResultId = matchResultId;
            NewOdd = newOdd;
        }
    }
}
