using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.Events
{
    public class SelectionResultChangedDomainEvent : INotification
    {
        public string SelectionId { get; }

        public SelectionResultChangedDomainEvent(string selectionId)
        {
            SelectionId = selectionId;

        }
    }
}
