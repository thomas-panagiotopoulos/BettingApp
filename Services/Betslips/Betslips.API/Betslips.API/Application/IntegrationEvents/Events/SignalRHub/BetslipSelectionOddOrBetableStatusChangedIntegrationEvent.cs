using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.IntegrationEvents.Events.SignalRHub
{
    public class BetslipSelectionOddOrBetableStatusChangedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetslipId { get; }
        public string SelectionId { get; }
        public decimal NewOdd { get; }
        public bool NewIsBetable { get; }

        public BetslipSelectionOddOrBetableStatusChangedIntegrationEvent(string gamblerId, string betslipId, 
                                                                string selectionId, decimal newOdd, bool newIsBetable)
        {
            GamblerId = gamblerId;
            BetslipId = betslipId;
            SelectionId = selectionId;
            NewOdd = newOdd;
            NewIsBetable = newIsBetable;
        }
    }
}
