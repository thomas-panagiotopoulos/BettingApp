using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.IntegrationEvents.Events.SignalRHub
{
    public class SportsbookMatchCanceledIntegrationEvent : IntegrationEvent
    {
        public string MatchId { get; }
        public SportsbookMatchCanceledIntegrationEvent(string matchId)
        {
            MatchId = matchId;
        }
    }
}
