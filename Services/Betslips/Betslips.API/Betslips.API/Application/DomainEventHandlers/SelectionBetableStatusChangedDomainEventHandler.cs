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
    public class SelectionBetableStatusChangedDomainEventHandler : INotificationHandler<SelectionBetableStatusChangedDomainEvent>
    {
        private readonly ILogger<SelectionBetableStatusChangedDomainEventHandler> _logger;
        private readonly IBetslipRepository _repository;
        private readonly IMediator _mediator;

        public SelectionBetableStatusChangedDomainEventHandler(
                                            ILogger<SelectionBetableStatusChangedDomainEventHandler> logger,
                                            IBetslipRepository repository,
                                            IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
        }
        public async Task Handle(SelectionBetableStatusChangedDomainEvent selectionChangedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A SelectionBetableStatusChangedDomainEvent is currently being handled...");

            // Load the Betslip that contains the updated Selection
            var betslip = await _repository.GetBySelectionIdAsync(selectionChangedEvent.SelectionId);

            _logger.LogInformation("A RecalculateBetslipBetableStatusCommand will be sent.");

            // Create and Send a RecalculateBetslipBetableStatusCommand using the mediator
            await _mediator.Send(new RecalculateBetslipBetableStatusCommand(betslip.Id));
        }
    }
}
