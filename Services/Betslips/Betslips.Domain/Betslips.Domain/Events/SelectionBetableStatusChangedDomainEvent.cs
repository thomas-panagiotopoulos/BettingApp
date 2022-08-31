using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Domain.Events
{
    public class SelectionBetableStatusChangedDomainEvent : INotification
    {
        public string SelectionId { get; }

        public SelectionBetableStatusChangedDomainEvent(string selectionId)
        {
            SelectionId = selectionId;
        }
    }
}
