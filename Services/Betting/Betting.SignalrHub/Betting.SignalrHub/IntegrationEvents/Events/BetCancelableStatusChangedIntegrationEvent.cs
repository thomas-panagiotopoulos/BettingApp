using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.SignalrHub.IntegrationEvents.Events
{
    public class BetCancelableStatusChangedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetId { get; }
        public bool NewIsCancelable { get; }

        public BetCancelableStatusChangedIntegrationEvent(string gamblerId, string betId, bool newIsCancelable)
        {
            GamblerId = gamblerId;
            BetId = betId;
            NewIsCancelable = newIsCancelable;
        }
    }
}
