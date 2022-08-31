using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.IntegrationEvents.Events
{
    public class MatchCurrentMinuteChangedIntegrationEvent : IntegrationEvent
    {
        public string MatchId { get; }

        public string NewCurrentMinute { get; }
        public MatchCurrentMinuteChangedIntegrationEvent(string matchId, string newCurrentMinute)
        {
            MatchId = matchId;
            NewCurrentMinute = newCurrentMinute;
        }
    }
}
