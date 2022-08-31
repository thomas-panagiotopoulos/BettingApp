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
    public class BetResultChangedDomainEventHandler : INotificationHandler<BetResultChangedDomainEvent>
    {
        private readonly ILogger<BetResultChangedDomainEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IBettingIntegrationEventService _bettingIntgrationEventService;

        public BetResultChangedDomainEventHandler(ILogger<BetResultChangedDomainEventHandler> logger,
                                             IBetRepository repository,
                                             IBettingIntegrationEventService bettingIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _bettingIntgrationEventService = bettingIntegrationEventService;
        }

        public async Task Handle(BetResultChangedDomainEvent betResultChangedEvent, CancellationToken cancellationToken)
        {
            var bet = await _repository.GetAsync(betResultChangedEvent.BetId);

            var betResultChangedIntegrationEvent = new BetResultChangedIntegrationEvent(bet.GamblerId, bet.Id, 
                                                                                            bet.ResultId, bet.ResultName);
            // Add a BetResultChangedIntegrationEvent to queue to be published latet
            // (receiver is the Betting.SignalRHub service)
            await _bettingIntgrationEventService.AddAndSaveEventAsync(betResultChangedIntegrationEvent);
        }
    }
}
