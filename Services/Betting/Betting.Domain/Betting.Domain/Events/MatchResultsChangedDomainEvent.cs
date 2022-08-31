using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.Events
{
    public class MatchResultsChangedDomainEvent : INotification
    {
        public string MatchId { get; }

        public MatchResultsChangedDomainEvent(string matchId)
        {
            MatchId = matchId;

        }
    }
}
