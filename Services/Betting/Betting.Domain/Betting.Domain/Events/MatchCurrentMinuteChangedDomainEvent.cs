using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.Events
{
    public class MatchCurrentMinuteChangedDomainEvent : INotification
    {
        public string MatchId { get; }
        public MatchCurrentMinuteChangedDomainEvent(string matchId)
        {
            MatchId = matchId;
        }
    }
}
