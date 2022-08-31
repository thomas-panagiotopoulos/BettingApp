using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Domain.Events
{
    public class MatchBetableStatusChangedDomainEvent : INotification
    {
        public string MatchId { get; }

        public MatchBetableStatusChangedDomainEvent(string matchId)
        {
            MatchId = matchId;
        }
    }
}
