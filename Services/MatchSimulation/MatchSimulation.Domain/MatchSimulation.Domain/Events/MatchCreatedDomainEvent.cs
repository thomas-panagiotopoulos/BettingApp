using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.Events
{
    public class MatchCreatedDomainEvent : INotification
    {
        public string MatchId { get; }
        public MatchCreatedDomainEvent(string matchId)
        {
            MatchId = matchId;
        }
    }
}
