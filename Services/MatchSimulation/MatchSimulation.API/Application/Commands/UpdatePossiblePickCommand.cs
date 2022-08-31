using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.Commands
{
    public class UpdatePossiblePickCommand : IRequest<Match>
    {
        [DataMember]
        public string MatchId { get; private set; }

        [DataMember]
        public int MatchResultId { get; private set; }

        [DataMember]
        public decimal NewOdd { get; private set; }

        [DataMember]
        public bool IsDisabled { get; private set; }

        public UpdatePossiblePickCommand(string matchId, int matchResultId, decimal newOdd, bool isDisabled)
        {
            MatchId = matchId;
            MatchResultId = matchResultId;
            NewOdd = newOdd;
            IsDisabled = isDisabled;
        }
    }
}
