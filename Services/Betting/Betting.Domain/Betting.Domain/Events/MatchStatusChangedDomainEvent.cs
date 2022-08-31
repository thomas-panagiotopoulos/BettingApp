using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.Events
{
    public class MatchStatusChangedDomainEvent : INotification
    {
        public string MatchId { get; }

        public MatchStatusChangedDomainEvent(string matchId)
        {
            MatchId = matchId;

        }
    }
}
