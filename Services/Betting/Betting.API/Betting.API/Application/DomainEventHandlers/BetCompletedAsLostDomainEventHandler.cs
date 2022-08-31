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
    public class BetCompletedAsLostDomainEventHandler : INotificationHandler<BetCompletedAsLostDomainEvent>
    {
        private readonly ILogger<BetCompletedAsLostDomainEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IBettingIntegrationEventService _bettingIntegrationEventService;

        public BetCompletedAsLostDomainEventHandler(ILogger<BetCompletedAsLostDomainEventHandler> logger,
                                                    IBetRepository repository,
                                                    IBettingIntegrationEventService bettingIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _bettingIntegrationEventService = bettingIntegrationEventService;
        }

        public async Task Handle(BetCompletedAsLostDomainEvent betLostEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A BetCompletedAsLostDomainEvent is currently being handled...");

            var bet = await _repository.GetAsync(betLostEvent.BetId);

            // Add a BetCompletedAsLostIntegrationEvent to the queue to be published later
            var betCompletedAsLostIntegrationEvent = new BetCompletedAsLostIntegrationEvent(bet.Id, bet.GamblerId);
            await _bettingIntegrationEventService.AddAndSaveEventAsync(betCompletedAsLostIntegrationEvent);

            _logger.LogInformation("A BetCompletedAsLostIntegrationEvent was added to queue to be published later.");
        }
    }
}
