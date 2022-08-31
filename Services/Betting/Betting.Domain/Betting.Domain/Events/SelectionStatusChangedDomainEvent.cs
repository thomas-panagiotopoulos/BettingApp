using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.Events
{
    public class SelectionStatusChangedDomainEvent : INotification
    {
        public string SelectionId { get; }


        public SelectionStatusChangedDomainEvent(string selectionId)
        {
            SelectionId = selectionId;

        }
    }
}
