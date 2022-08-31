using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.Events
{
    public class BetDetailsChangedDomainEvent : INotification
    {
        public string BetId { get; }
        public decimal OldTotalOdd { get; }

        public BetDetailsChangedDomainEvent(string betId, decimal oldTotalodd)
        {
            BetId = betId;
            OldTotalOdd = oldTotalodd;
        }
    }
}
