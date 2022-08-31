using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Domain.Events
{
    public class BetslipBetableStatusChangedDomainEvent : INotification
    {
        public string BetslipId { get; }

        public BetslipBetableStatusChangedDomainEvent(string betslipId)
        {
            BetslipId = betslipId;
        }
    }
}
