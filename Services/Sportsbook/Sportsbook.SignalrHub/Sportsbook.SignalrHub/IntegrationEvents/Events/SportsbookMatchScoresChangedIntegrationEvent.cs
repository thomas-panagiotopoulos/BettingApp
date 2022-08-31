using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportsbook.SignalrHub.IntegrationEvents.Events
{
    public class SportsbookMatchScoresChangedIntegrationEvent : IntegrationEvent
    {
        public string MatchId { get; }

        public int NewHomeClubScore { get; }

        public int NewAwayClubScore { get; }

        public SportsbookMatchScoresChangedIntegrationEvent(string matchId, int newHomeClubScore, int newAwayClubScore)
        {
            MatchId = matchId;
            NewHomeClubScore = newHomeClubScore;
            NewAwayClubScore = newAwayClubScore;
        }
    }
}
