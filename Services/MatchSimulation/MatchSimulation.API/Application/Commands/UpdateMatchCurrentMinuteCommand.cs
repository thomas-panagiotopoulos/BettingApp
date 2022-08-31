using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.Commands
{
    public class UpdateMatchCurrentMinuteCommand : IRequest<Match>
    {
        [DataMember]
        public string MatchId { get; private set; }

        [DataMember]
        public string NewCurrentMinute { get; private set; }

        public UpdateMatchCurrentMinuteCommand(string matchId, string newCurrentMinute)
        {
            MatchId = matchId;
            NewCurrentMinute = newCurrentMinute;
        }
    }
}
