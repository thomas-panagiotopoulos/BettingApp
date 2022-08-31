using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.Events
{
    public class SimulationCreatedDomainEvent : INotification
    {
        public string MatchId { get; }
        public string SimulationId { get; }
        public Club HomeClub { get; }
        public Club AwayClub { get; }
        public int LeagueId { get; }
        public DateTime KickoffDateTime { get; }
        public List<PossiblePick> PossiblePicks { get; }

        public SimulationCreatedDomainEvent(string matchId, string simulationId, Club homeClub, Club awayClub,
                                            int leagueId, DateTime kickoffDateTime, List<PossiblePick> possiblePicks)
        {
            MatchId = matchId;
            SimulationId = simulationId;
            HomeClub = homeClub;
            AwayClub = awayClub;
            LeagueId = leagueId;
            KickoffDateTime = kickoffDateTime;
            PossiblePicks = possiblePicks;
        }
    }
}
