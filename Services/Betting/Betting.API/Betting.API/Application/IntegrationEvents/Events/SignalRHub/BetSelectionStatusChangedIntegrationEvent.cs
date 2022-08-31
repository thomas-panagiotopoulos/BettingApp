using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.IntegrationEvents.Events.SignalRHub
{
    public class BetSelectionStatusChangedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetId { get; }
        public string SelectionId { get; }
        public int NewStatusId { get; }
        public string NewStatusName { get; }

        public BetSelectionStatusChangedIntegrationEvent(string gamblerId, string betId, string selectionId,
                                                        int newStatusId, string newStatusName)
        {
            GamblerId = gamblerId;
            BetId = betId;
            SelectionId = selectionId;
            NewStatusId = newStatusId;
            NewStatusName = newStatusName;
        }
    }
}
