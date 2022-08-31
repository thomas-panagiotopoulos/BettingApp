using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.IntegrationEvents.Events.SignalRHub
{
    public class BetslipSelectionMatchCanceledIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetslipId { get; }
        public string SelectionId { get; }

        public BetslipSelectionMatchCanceledIntegrationEvent(string gamblerId, string betslipId, string selectionId)
        {
            GamblerId = gamblerId;
            BetslipId = betslipId;
            SelectionId = selectionId;
        }
    }
}
