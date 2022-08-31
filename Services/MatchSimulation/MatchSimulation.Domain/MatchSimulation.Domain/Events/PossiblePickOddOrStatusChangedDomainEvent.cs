using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.Events
{
    public class PossiblePickOddOrStatusChangedDomainEvent : INotification
    {
        public string MatchId { get; }
        public int MatchResultId { get; }
        public decimal NewOdd { get; }
        public bool IsDisabled { get; }
        public PossiblePickOddOrStatusChangedDomainEvent(string matchId, int matchResultId, decimal newOdd, bool isDisabled)
        {
            MatchId = matchId;
            MatchResultId = matchResultId;
            NewOdd = newOdd;
            IsDisabled = isDisabled;
        }
    }
}
