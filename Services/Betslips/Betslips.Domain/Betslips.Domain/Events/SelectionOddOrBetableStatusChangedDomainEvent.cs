using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Domain.Events
{
    public class SelectionOddOrBetableStatusChangedDomainEvent : INotification
    {
        public string SelectionId { get; }

        public SelectionOddOrBetableStatusChangedDomainEvent(string selectionId)
        {
            SelectionId = selectionId;
        }
    }
}
