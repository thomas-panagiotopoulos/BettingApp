using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.Events
{
    public class MatchNewCurrentMinuteCalculatedDomainEvent : INotification
    {
        public string MatchId { get; }

        public string NewCurrentMinute { get; }

        public MatchNewCurrentMinuteCalculatedDomainEvent(string matchId, string newCurrentMinute)
        {
            MatchId = matchId;
            NewCurrentMinute = newCurrentMinute;
        }
    }
}
