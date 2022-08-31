using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.Events
{
    public class BetCompletedAsLostDomainEvent : INotification
    {
        public string BetId { get; }

        public BetCompletedAsLostDomainEvent(string betId)
        {
            BetId = betId;
        }
    }
}
