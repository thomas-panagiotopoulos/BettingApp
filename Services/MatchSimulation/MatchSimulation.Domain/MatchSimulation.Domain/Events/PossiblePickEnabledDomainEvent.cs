using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.Events
{
    public class PossiblePickEnabledDomainEvent : INotification
    {
        public string MatchId { get; }
        public int MatchResultId { get; }
        public PossiblePickEnabledDomainEvent(string matchId, int matchResultId)
        {
            MatchId = matchId;
            MatchResultId = matchResultId;
        }
    }
}
