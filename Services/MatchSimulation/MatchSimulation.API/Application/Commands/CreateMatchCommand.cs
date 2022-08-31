using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.Commands
{
    public class CreateMatchCommand : IRequest<Match>
    {
        [DataMember]
        public string MatchId { get; private set; }

        [DataMember]
        public string SimulationId { get; private set; }

        [DataMember]
        public Club HomeClub { get; private set; }

        [DataMember]
        public Club AwayClub { get; private set; }

        [DataMember]
        public int LeagueId { get; private set; }

        [DataMember]
        public DateTime KickoffDateTime { get; private set; }

        [DataMember]
        public List<PossiblePick> PossiblePicks { get; private set; }


        public CreateMatchCommand(string matchId, string simulationId, Club homeClub, Club awayClub, int leagueId, 
                                    DateTime kickoffDateTime, List<PossiblePick> possiblePicks)
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
