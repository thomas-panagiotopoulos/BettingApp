using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.SignalrHub.IntegrationEvents.Events
{
    public class BetSelectionResultChangedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetId { get; }
        public string SelectionId { get; }
        public int NewResultId { get; }
        public string NewResultName { get; }

        public BetSelectionResultChangedIntegrationEvent(string gamblerId, string betId, string selectionId,
                                                        int newResultId, string newResultName)
        {
            GamblerId = gamblerId;
            BetId = betId;
            SelectionId = selectionId;
            NewResultId = newResultId;
            NewResultName = newResultName;
        }
    }
}
