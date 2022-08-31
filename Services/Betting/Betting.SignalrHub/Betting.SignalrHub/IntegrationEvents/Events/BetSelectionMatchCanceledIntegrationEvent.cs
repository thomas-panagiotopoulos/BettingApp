using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.SignalrHub.IntegrationEvents.Events
{
    public class BetSelectionMatchCanceledIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetId { get; }
        public string SelectionId { get; }

        public BetSelectionMatchCanceledIntegrationEvent(string gamblerId, string betId, string selectionId)
        {
            GamblerId = gamblerId;
            BetId = betId;
            SelectionId = selectionId;
        }
    }
}
