using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Domain.Events
{
    public class RequirementFulfillmentStatusChangedDomainEvent : INotification
    {
        public string RequirementId { get; }

        public RequirementFulfillmentStatusChangedDomainEvent(string requirementId)
        {
            RequirementId = requirementId;
        }
    }
}
