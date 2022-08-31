using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.TestingService.API.IntegrationEvents.Events
{
    public class UserCheckedOutIntegrationEvent : IntegrationEvent
    {
        public Guid RequestId { get; }

        public BetslipDTO BetslipDTO { get; }

        public UserCheckedOutIntegrationEvent(Guid requestId, BetslipDTO betslipDto)
        {
            RequestId = requestId;
            BetslipDTO = betslipDto;
        }
    }

    public class BetslipDTO
    {
        public string GamblerId { get; set; }
        public decimal WageredAmount { get; set; }
        public List<SelectionDTO> SelectionDTOs { get; set; }
    }

}
