using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.Events
{
    public class MatchCurrentMinuteChangedDomainEvent : INotification
    {
        public string MatchId { get; }

        public string NewCurrentMinute { get; }

        public MatchCurrentMinuteChangedDomainEvent(string matchId, string newCurrentMinute)
        {
            MatchId = matchId;
            NewCurrentMinute = newCurrentMinute;
        }
    }
}
