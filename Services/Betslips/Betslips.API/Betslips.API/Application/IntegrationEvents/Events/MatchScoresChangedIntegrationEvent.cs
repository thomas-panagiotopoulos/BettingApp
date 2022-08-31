using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.IntegrationEvents.Events
{
    public class MatchScoresChangedIntegrationEvent : IntegrationEvent
    {
        public string MatchId { get; }

        public int NewHomeClubScore { get; }

        public int NewAwayClubScore { get; }

        public MatchScoresChangedIntegrationEvent(string matchId, 
                                                  int newHomeClubScore, int newAwayClubScore)
        {
            MatchId = matchId;
            NewHomeClubScore = newHomeClubScore;
            NewAwayClubScore = newAwayClubScore;
        }
    }
}
