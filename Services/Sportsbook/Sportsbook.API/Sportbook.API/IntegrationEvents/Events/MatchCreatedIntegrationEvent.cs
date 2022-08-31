using BettingApp.BuildingBlocks.EventBus.Events;
using BettingApp.Services.Sportbook.API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.IntegrationEvents.Events
{
    public class MatchCreatedIntegrationEvent : IntegrationEvent
    {
        public MatchDTO MatchDTO { get; set; }

        public MatchCreatedIntegrationEvent(MatchDTO matchDto)
        {
            MatchDTO = matchDto;
        }
    }
}
