using BettingApp.BuildingBlocks.EventBus.Events;
using BettingApp.Services.Betslips.API.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.IntegrationEvents.Events
{
    public class UserRequestedToAddSelectionIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }

        public SelectionDTO SelectionDTO { get; }

        public string LatestAdditionId { get; }

        public UserRequestedToAddSelectionIntegrationEvent(string gamblerId, SelectionDTO selectionDto, string latestAdditionId)
        {
            GamblerId = gamblerId;
            SelectionDTO = selectionDto;
            LatestAdditionId = latestAdditionId;
        }
    }
}
