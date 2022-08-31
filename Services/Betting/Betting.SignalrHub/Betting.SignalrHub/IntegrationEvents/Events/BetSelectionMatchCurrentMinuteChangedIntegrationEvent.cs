using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.SignalrHub.IntegrationEvents.Events
{
    public class BetSelectionMatchCurrentMinuteChangedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetId { get; }
        public string SelectionId { get; }
        public string NewCurrentMinute { get; }

        public BetSelectionMatchCurrentMinuteChangedIntegrationEvent(string gamblerId, string betId, string selectionId,
                                                        string newCurrentMinute)
        {
            GamblerId = gamblerId;
            BetId = betId;
            SelectionId = selectionId;
            NewCurrentMinute = newCurrentMinute;
        }
    }
}
