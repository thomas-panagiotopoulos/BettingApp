using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.SignalrHub.IntegrationEvents.Events
{
    public class BetslipBetableStatusChangedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetslipId { get; }
        public bool NewIsBetable { get; }

        public BetslipBetableStatusChangedIntegrationEvent(string gamblerId, string betslipId, bool newIsBetable)
        {
            GamblerId = gamblerId;
            BetslipId = betslipId;
            NewIsBetable = newIsBetable;
        }
    }
}
