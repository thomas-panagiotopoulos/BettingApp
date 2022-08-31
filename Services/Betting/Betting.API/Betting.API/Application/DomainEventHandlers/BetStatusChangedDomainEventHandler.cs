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
    public class BetStatusChangedDomainEventHandler : INotificationHandler<BetStatusChangedDomainEvent>
    {
        private readonly ILogger<BetStatusChangedDomainEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IBettingIntegrationEventService _bettingIntgrationEventService;

        public BetStatusChangedDomainEventHandler(ILogger<BetStatusChangedDomainEventHandler> logger,
                                             IBetRepository repository,
                                             IBettingIntegrationEventService bettingIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _bettingIntgrationEventService = bettingIntegrationEventService;
        }

        public async Task Handle(BetStatusChangedDomainEvent betStatusChangedEvent, CancellationToken cancellationToken)
        {
            var bet = await _repository.GetAsync(betStatusChangedEvent.BetId);

            var betStatusChangedIntegrationEvent = new BetStatusChangedIntegrationEvent(bet.GamblerId, bet.Id,
                                                                                        bet.StatusId, bet.StatusName);
            // Add a BetStatusChangedIntegrationEvent to queue to be published latet
            // (receiver is the Betting.SignalRHub service)
            await _bettingIntgrationEventService.AddAndSaveEventAsync(betStatusChangedIntegrationEvent);
        }
    }
}
