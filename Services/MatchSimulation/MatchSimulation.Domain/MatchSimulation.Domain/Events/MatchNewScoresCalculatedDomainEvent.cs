using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.Events
{
    public class MatchNewScoresCalculatedDomainEvent : INotification
    {
        public string MatchId { get; }
        public int NewHomeClubScore { get; }
        public int NewAwayClubScore { get; }

        public MatchNewScoresCalculatedDomainEvent(string matchId, int newHomeClubScore, int newAwayClubScore)
        {
            MatchId = matchId;
            NewHomeClubScore = newHomeClubScore;
            NewAwayClubScore = newAwayClubScore;
        }
    }
}
