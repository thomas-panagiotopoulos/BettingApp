using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.IntegrationEvents.Events.SignalRHub
{
    public class BetSelectionMatchScoresChangedIntegrationEvent : IntegrationEvent
    {
        public string GamblerId { get; }
        public string BetId { get; }
        public string SelectionId { get; }
        public int NewHomeClubScore { get; }
        public int NewAwayClubScore { get; }

        public BetSelectionMatchScoresChangedIntegrationEvent(string gamblerId, string betId, string selectionId,
                                                        int newHomeClubScore, int newAwayClubScore)
        {
            GamblerId = gamblerId;
            BetId = betId;
            SelectionId = selectionId;
            NewHomeClubScore = newHomeClubScore;
            NewAwayClubScore = newAwayClubScore;
        }
    }
}
