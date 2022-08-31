using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SimulationAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.Commands
{
    public class CreateSimulationCommand : IRequest<Simulation>
    {
        [DataMember]
        public string MatchId { get; private set; }

        [DataMember]
        public Club HomeClub { get; private set; }

        [DataMember]
        public Club AwayClub { get; private set; }

        [DataMember]
        public int LeagueId { get; private set; }

        [DataMember]
        public DateTime KickoffDateTime { get; private set; }

        public CreateSimulationCommand(string matchId, Club homeClub, Club awayClub, int leagueId, DateTime kickoffDateTime)
        {
            MatchId = matchId;
            HomeClub = homeClub;
            AwayClub = awayClub;
            LeagueId = leagueId;
            KickoffDateTime = kickoffDateTime;
        }
    }
}
