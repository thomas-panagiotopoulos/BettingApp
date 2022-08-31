using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.SignalrHub.IntegrationEvents.Events
{
    public class BetslipSelectionMatchCurrentMinuteChangedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetslipId { get; }
        public string SelectionId { get; }
        public string NewCurrentMinute { get; }

        public BetslipSelectionMatchCurrentMinuteChangedIntegrationEvent(string gamblerId, string betslipId, string selectionId, string newCurrentMinute)
        {
            GamblerId = gamblerId;
            BetslipId = betslipId;
            SelectionId = selectionId;
            NewCurrentMinute = newCurrentMinute;
        }
    }
}
