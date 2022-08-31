using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.IntegrationEvents.Events.SignalRHub
{
    public class BetslipSelectionMatchScoresChangedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetslipId { get; }
        public string SelectionId { get; }
        public int NewHomeClubScore { get; }
        public int NewAwayClubScore { get; }

        public BetslipSelectionMatchScoresChangedIntegrationEvent(string gamblerId, string betslipId, string selectionId, 
                                                            int newHomeClubScore, int newAwayClubScore)
        {
            GamblerId = gamblerId;
            BetslipId = betslipId;
            SelectionId = selectionId;
            NewHomeClubScore = newHomeClubScore;
            NewAwayClubScore = newAwayClubScore;
        }
    }
}
