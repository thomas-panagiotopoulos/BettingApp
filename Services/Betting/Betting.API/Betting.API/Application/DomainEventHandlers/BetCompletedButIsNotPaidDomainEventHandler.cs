using BettingApp.Services.Betting.API.Application.Commands;
using BettingApp.Services.Betting.API.Application.IntegrationEvents;
using BettingApp.Services.Betting.API.Application.IntegrationEvents.Events;
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
    public class BetCompletedButIsNotPaidDomainEventHandler : INotificationHandler<BetCompletedButIsNotPaidDomainEvent>
    {
        private readonly ILogger<BetCompletedButIsNotPaidDomainEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IBettingIntegrationEventService _bettingIntegrationEventService;
        private readonly IMediator _mediator;

        public BetCompletedButIsNotPaidDomainEventHandler(ILogger<BetCompletedButIsNotPaidDomainEventHandler> logger,
                                                   IBetRepository repository,
                                                   IBettingIntegrationEventService bettingIntegrationEventService,
                                                   IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _bettingIntegrationEventService = bettingIntegrationEventService;
            _mediator = mediator;
        }

        public async Task Handle(BetCompletedButIsNotPaidDomainEvent betCompletedNotPaidEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A BetCompletedButIsNotPaidDomainEvent is currently being handled...");

            var bet = await _repository.GetAsync(betCompletedNotPaidEvent.BetId);

            // Add a BetRemovedDueToUnpaidCompletionIntegrationEvent to the queue to be published later
            var betRemovedIntegrationEvent = new BetRemovedDueToUnpaidCompletionIntegrationEvent(bet.Id, bet.GamblerId);
            await _bettingIntegrationEventService.AddAndSaveEventAsync(betRemovedIntegrationEvent);
            _logger.LogInformation("A BetRemovedDueToUnpaidCompletionIntegrationEvent was added to queue to be published later.");

            // Send a RemoveBetCommand to completely remove the Bet from the DB
            _logger.LogInformation("A RemoveBetCommand will be sent.");
            await _mediator.Send(new RemoveBetCommand(bet.Id));
        }
    }
}
