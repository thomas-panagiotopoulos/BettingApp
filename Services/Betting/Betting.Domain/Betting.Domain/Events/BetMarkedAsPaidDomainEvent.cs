using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.Events
{
    public class BetMarkedAsPaidDomainEvent : INotification
    {
        public string BetId { get; }

        public BetMarkedAsPaidDomainEvent(string betId)
        {
            BetId = betId;
        }
    }
}
