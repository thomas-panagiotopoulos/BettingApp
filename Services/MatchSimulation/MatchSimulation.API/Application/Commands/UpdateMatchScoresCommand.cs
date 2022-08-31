using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.Commands
{
    public class UpdateMatchScoresCommand : IRequest<Match>
    {
        [DataMember]
        public string MatchId { get; private set; }

        [DataMember]
        public int NewHomeClubScore { get; private set; }

        [DataMember]
        public int NewAwayClubScore { get; private set; }

        public UpdateMatchScoresCommand(string matchId, int newHomeClubScore, int newAwayClubScore)
        {
            MatchId = matchId;
            NewHomeClubScore = newHomeClubScore;
            NewAwayClubScore = newAwayClubScore;
        }
    }
}
