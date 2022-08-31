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
    public class BetCompletedAsWonDomainEventHandler : INotificationHandler<BetCompletedAsWonDomainEvent>
    {
        private readonly ILogger<BetCompletedAsWonDomainEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IBettingIntegrationEventService _bettingIntegrationEventService;

        public BetCompletedAsWonDomainEventHandler(ILogger<BetCompletedAsWonDomainEventHandler> logger,
                                                   IBetRepository repository,
                                                   IBettingIntegrationEventService bettingIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _bettingIntegrationEventService = bettingIntegrationEventService;
        }

        public async Task Handle(BetCompletedAsWonDomainEvent betWonEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A BetCompletedAsWonDomainEvent is currently being handled...");

            var bet = await _repository.GetAsync(betWonEvent.BetId);

            // Add a BetCompletedAsWonIntegrationEvent to the queue to be published later
            var betCompletedAsWonIntegrationEvent = new BetCompletedAsWonIntegrationEvent(bet.Id, bet.GamblerId, 
                                                                                          bet.PotentialWinnings);
            await _bettingIntegrationEventService.AddAndSaveEventAsync(betCompletedAsWonIntegrationEvent);

            _logger.LogInformation("A BetCompletedAsWonIntegrationEvent was added to queue to be published later.");

        }
    }
}
