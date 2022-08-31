using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.Events
{
    public class PossiblePickNewOddAndStatusCalculatedDomainEvent : INotification
    {
        public string MatchId { get; }
        public int MatchResultId { get; }
        public decimal NewOdd { get; }
        public bool IsExtremeValue { get; }

        public PossiblePickNewOddAndStatusCalculatedDomainEvent(string matchId, int matchResultId, decimal newOdd, bool isExtremeValue)
        {
            MatchId = matchId;
            MatchResultId = matchResultId;
            NewOdd = newOdd;
            IsExtremeValue = isExtremeValue;
        }
    }
}
