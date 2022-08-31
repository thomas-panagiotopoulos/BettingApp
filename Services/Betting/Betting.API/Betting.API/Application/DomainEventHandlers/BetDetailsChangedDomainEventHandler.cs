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
    public class BetDetailsChangedDomainEventHandler : INotificationHandler<BetDetailsChangedDomainEvent>
    {
        private readonly ILogger<BetDetailsChangedDomainEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IBettingIntegrationEventService _bettingIntegrationEventService;

        public BetDetailsChangedDomainEventHandler(ILogger<BetDetailsChangedDomainEventHandler> logger,
                                                    IBetRepository repository,
                                                    IBettingIntegrationEventService bettingIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _bettingIntegrationEventService = bettingIntegrationEventService;
        }

        public async Task Handle(BetDetailsChangedDomainEvent betDetailsChangedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A BetDetailsChangedDomainEvent is currently being handled...");

            var bet = await _repository.GetAsync(betDetailsChangedEvent.BetId);

            // Add a BetTotalOddChangedIntegrationEvent to the queue to be published later
            // note: this integration event notifies both Notifications service and Betting.SignalR service
            var betDetailsChangedIntegrationEvent = new BetDetailsChangedIntegrationEvent(bet.GamblerId, bet.Id, 
                                                                                betDetailsChangedEvent.OldTotalOdd,
                                                                                bet.TotalOdd, bet.PotentialWinnings, 
                                                                                bet.PotentialProfit);
            await _bettingIntegrationEventService.AddAndSaveEventAsync(betDetailsChangedIntegrationEvent);

            _logger.LogInformation("A BetDetailsChangedIntegrationEvent was added to queue to be published later.");
        }
    }
}
