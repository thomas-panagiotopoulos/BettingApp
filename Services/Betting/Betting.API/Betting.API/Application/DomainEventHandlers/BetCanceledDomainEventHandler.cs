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
    public class BetCanceledDomainEventHandler : INotificationHandler<BetCanceledDomainEvent>
    {
        private readonly ILogger<BetCanceledDomainEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IBettingIntegrationEventService _bettingIntegrationEventService;

        public BetCanceledDomainEventHandler(ILogger<BetCanceledDomainEventHandler> logger,
                                             IBetRepository repository,
                                             IBettingIntegrationEventService bettingIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _bettingIntegrationEventService = bettingIntegrationEventService;
        }

        public async Task Handle(BetCanceledDomainEvent betCanceledEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A BetCanceledDomainEvent is currently being handled...");

            var bet = await _repository.GetAsync(betCanceledEvent.BetId);

            // Add a BetCanceledIntergrationEvent to the queue to be published later
            var betCanceledIntegrationEvent = new BetCanceledIntegrationEvent(bet.Id, bet.GamblerId, bet.WageredAmount);
            await _bettingIntegrationEventService.AddAndSaveEventAsync(betCanceledIntegrationEvent);

            _logger.LogInformation("A BetCanceledIntergrationEvent was added to queue to be published later.");
        }
    }
}
