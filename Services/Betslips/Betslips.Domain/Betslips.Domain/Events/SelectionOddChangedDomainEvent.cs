using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Domain.Events
{
    public class SelectionOddChangedDomainEvent : INotification
    {
        public string SelectionId { get; }

        public SelectionOddChangedDomainEvent(string selectionId)
        {
            SelectionId = selectionId;
        }
    }
}
