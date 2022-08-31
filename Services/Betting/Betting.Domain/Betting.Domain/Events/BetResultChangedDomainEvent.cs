using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.Events
{
    public class BetResultChangedDomainEvent : INotification
    {
        public string BetId { get; }
        public BetResultChangedDomainEvent(string betId)
        {
            BetId = betId;
        }
    }
}
