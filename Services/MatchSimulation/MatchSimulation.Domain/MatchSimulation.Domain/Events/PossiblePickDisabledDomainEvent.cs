using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.Events
{
    public class PossiblePickDisabledDomainEvent : INotification
    {
        public string MatchId { get; }
        public int MatchResultId { get; }
        public PossiblePickDisabledDomainEvent(string matchId, int matchResultId)
        {
            MatchId = matchId;
            MatchResultId = matchResultId;
        }
    }
}
