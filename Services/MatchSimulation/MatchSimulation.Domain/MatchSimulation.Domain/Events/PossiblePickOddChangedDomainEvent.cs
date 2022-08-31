using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.Events
{
    public class PossiblePickOddChangedDomainEvent : INotification
    {
        public string MatchId { get; }
        public int MatchResultId { get; }
        public decimal NewOdd { get; }
        public PossiblePickOddChangedDomainEvent(string matchId, int matchResultId, decimal newOdd)
        {
            MatchId = matchId;
            MatchResultId = matchResultId;
            NewOdd = newOdd;
        }
    }
}
