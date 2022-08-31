using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.Events
{
    public class BetCanceledDomainEvent : INotification
    {
        public string BetId { get; }
        
        public BetCanceledDomainEvent(string betId)
        {
            BetId = betId;
            
        }
    }
}
