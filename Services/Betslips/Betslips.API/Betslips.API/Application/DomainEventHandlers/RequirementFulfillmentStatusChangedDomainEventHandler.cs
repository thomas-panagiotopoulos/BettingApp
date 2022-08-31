using BettingApp.Services.Betslips.API.Application.Commands;
using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using BettingApp.Services.Betslips.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.DomainEventHandlers
{
    public class RequirementFulfillmentStatusChangedDomainEventHandler
        : INotificationHandler<RequirementFulfillmentStatusChangedDomainEvent>
    {
        private readonly ILogger<RequirementFulfillmentStatusChangedDomainEventHandler> _logger;
        private readonly IBetslipRepository _repository;
        private readonly IMediator _mediator;

        public RequirementFulfillmentStatusChangedDomainEventHandler(
                        ILogger<RequirementFulfillmentStatusChangedDomainEventHandler> logger,
                        IBetslipRepository repository,
                        IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(RequirementFulfillmentStatusChangedDomainEvent requirementChangedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A RequirementFulfillmentStatusChangedDomainEvent is currently being handled...");

            // Load the Betslip that contains the updated Requirement
            var betslip = await _repository.GetByRequirementIdAsync(requirementChangedEvent.RequirementId);

            // Find the Selection to recalculate Betable status
            var selection = betslip.Selections
                                   .FirstOrDefault(s => s.Requirement.Id.Equals(requirementChangedEvent.RequirementId));

            _logger.LogInformation("A RecalculateSelectionBetableStatusCommand will be sent.");

            // Create and Send a RecalculateSelectionBetableStatusCommand using the mediator
            await _mediator.Send(new RecalculateSelectionBetableStatusCommand(selection.Id));
        }
    }
}
