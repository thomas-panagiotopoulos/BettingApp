using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.IntegrationEvents.Events
{
    public class MatchResultOddOrStatusChangedIntegrationEvent : IntegrationEvent
    {
        public string MatchId { get; }

        public int MatchResultId { get; }

        public decimal NewOdd { get; }

        public bool IsDisabled { get; }

        public MatchResultOddOrStatusChangedIntegrationEvent(string matchId, int matchResultId, decimal newOdd, bool isDisabled)
        {
            MatchId = matchId;
            MatchResultId = matchResultId;
            NewOdd = newOdd;
            IsDisabled = isDisabled;
        }
    }
}
