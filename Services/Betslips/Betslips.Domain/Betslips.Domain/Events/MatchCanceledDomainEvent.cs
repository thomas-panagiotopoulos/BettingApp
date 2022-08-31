using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Domain.Events
{
    public class MatchCanceledDomainEvent : INotification
    {
        public string MatchId { get; }

        public MatchCanceledDomainEvent(string matchId)
        {
            MatchId = matchId;
        }
    }
}
