using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.TestingService.API.IntegrationEvents.Events
{
    public class UserRequestedToAddSelectionIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }

        public SelectionDTO SelectionDTO { get; }

        public UserRequestedToAddSelectionIntegrationEvent(string gamblerId, SelectionDTO selectionDto)
        {
            GamblerId = gamblerId;
            SelectionDTO = selectionDto;
        }
    }

    public class SelectionDTO
    {
        public int GamblerMatchResultId { get; set; }

        public decimal Odd { get; set; }

        public string RelatedMatchId { get; set; }

        public string HomeClubName { get; set; }

        public string AwayClubName { get; set; }

        public DateTime KickoffDateTime { get; set; }

        public string CurrentMinute { get; set; }

        public int HomeClubScore { get; set; }

        public int AwayClubScore { get; set; }

        public int RequirementTypeId { get; set; }

        public decimal RequiredValue { get; set; }

    }
}
