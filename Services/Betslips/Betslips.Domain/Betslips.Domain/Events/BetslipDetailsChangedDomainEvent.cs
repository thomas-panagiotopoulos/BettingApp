using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Domain.Events
{
    public class BetslipDetailsChangedDomainEvent : INotification
    {
        public string BetslipId { get; }

        public BetslipDetailsChangedDomainEvent(string betslipId)
        {
            BetslipId = betslipId;
        }
    }
}
