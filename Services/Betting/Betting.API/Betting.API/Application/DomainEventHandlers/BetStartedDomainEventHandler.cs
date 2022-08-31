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
    public class BetStartedDomainEventHandler : INotificationHandler<BetStartedDomainEvent>
    {
        private readonly ILogger<BetStartedDomainEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IBettingIntegrationEventService _bettingIntgrationEventService;

        public BetStartedDomainEventHandler(ILogger<BetStartedDomainEventHandler> logger,
                                             IBetRepository repository,
                                             IBettingIntegrationEventService bettingIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _bettingIntgrationEventService = bettingIntegrationEventService;
        }

        public async Task Handle(BetStartedDomainEvent betStartedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A BetStartedDomainEvent is currently being handled...");

            var bet = await _repository.GetAsync(betStartedEvent.BetId);

            // Add a BetStartedIntegrationEvent to the queue to be published later
            var betStartedIntegrationEvent = new BetStartedIntegrationEvent(bet.Id, bet.GamblerId, bet.WageredAmount);
            await _bettingIntgrationEventService.AddAndSaveEventAsync(betStartedIntegrationEvent);

            _logger.LogInformation("A BetStartedIntergrationEvent was added to queue to be published later.");
        }
    }
}
