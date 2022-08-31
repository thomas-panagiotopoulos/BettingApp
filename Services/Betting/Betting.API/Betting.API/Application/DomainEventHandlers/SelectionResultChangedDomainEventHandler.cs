using BettingApp.Services.Betting.API.Application.Commands;
using BettingApp.Services.Betting.API.Application.IntegrationEvents;
using BettingApp.Services.Betting.API.Application.IntegrationEvents.Events.SignalRHub;
using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using BettingApp.Services.Betting.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.DomainEventHandlers
{
    public class SelectionResultChangedDomainEventHandler : INotificationHandler<SelectionResultChangedDomainEvent>
    {
        private readonly ILogger<SelectionResultChangedDomainEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IMediator _mediator;
        private readonly IBettingIntegrationEventService _bettingIntegrationEventService;

        public SelectionResultChangedDomainEventHandler(ILogger<SelectionResultChangedDomainEventHandler> logger,
                                                     IBetRepository repository,
                                                     IMediator mediator,
                                                     IBettingIntegrationEventService bettingIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
            _bettingIntegrationEventService = bettingIntegrationEventService;
        }

        public async Task Handle(SelectionResultChangedDomainEvent selectionChangedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A SelectionResultChangedDomainEvent is currently being handled...");

            // load Bet that contains the updated Selection using the repository
            var bet = await _repository.GetBySelectionIdAsync(selectionChangedEvent.SelectionId);

            _logger.LogInformation("A RecalculateBetResultCommand will be sent.");

            // create and send a RecalculateBetResultCommand using the mediator
            await _mediator.Send(new RecalculateBetResultCommand(bet.Id));

            // Add a BetSelectionResultChangedIntegrationEvent to queue to be published later
            // (receiver is the Betting.SignalRHub service)
            var selection = bet.Selections.FirstOrDefault(s => s.Id.Equals(selectionChangedEvent.SelectionId));

            var betSelectionResultChangedIntegrationEvent = new BetSelectionResultChangedIntegrationEvent(bet.GamblerId,
                                                                                bet.Id, selection.Id, selection.ResultId,
                                                                                selection.ResultName);

            await _bettingIntegrationEventService.AddAndSaveEventAsync(betSelectionResultChangedIntegrationEvent);
        }
    }
}
