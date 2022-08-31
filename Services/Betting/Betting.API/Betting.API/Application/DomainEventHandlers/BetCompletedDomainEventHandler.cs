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
    public class BetCompletedDomainEventHandler : INotificationHandler<BetCompletedDomainEvent>
    {
        private readonly ILogger<BetCompletedDomainEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IBettingIntegrationEventService _bettingIntegrationEventService;

        public BetCompletedDomainEventHandler(ILogger<BetCompletedDomainEventHandler> logger,
                                                   IBetRepository repository,
                                                   IBettingIntegrationEventService bettingIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _bettingIntegrationEventService = bettingIntegrationEventService;
        }

        public async Task Handle(BetCompletedDomainEvent betCompletedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A BetCompletedDomainEvent is currently being handled...");

            var bet = await _repository.GetAsync(betCompletedEvent.BetId);

            // Add a BetCompletedIntegrationEvent to the queue to be published later
            if (bet.ResultId == BettingResult.Won.Id)
            {
                var betCompletedIntegrationEvent = new BetCompletedIntegrationEvent(bet.Id, bet.GamblerId, 
                                                                                    true, bet.PotentialWinnings);
                await _bettingIntegrationEventService.AddAndSaveEventAsync(betCompletedIntegrationEvent);
            }
            else if (bet.ResultId == BettingResult.Lost.Id)
            {
                var betCompletedIntegrationEvent = new BetCompletedIntegrationEvent(bet.Id, bet.GamblerId, 
                                                                                    false, 0);
                await _bettingIntegrationEventService.AddAndSaveEventAsync(betCompletedIntegrationEvent);
            }

            _logger.LogInformation("A BetCompletedIntegrationEvent was added to queue to be published later.");
        }
    }
}
