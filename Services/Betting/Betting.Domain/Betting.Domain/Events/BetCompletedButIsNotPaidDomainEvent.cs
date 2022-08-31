using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.Events
{
    public class BetCompletedButIsNotPaidDomainEvent : INotification
    {
        public string BetId { get; }

        public BetCompletedButIsNotPaidDomainEvent(string betId)
        {
            BetId = betId;
        }
    }
}
