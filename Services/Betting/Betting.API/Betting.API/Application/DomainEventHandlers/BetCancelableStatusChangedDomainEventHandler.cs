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
    public class BetCancelableStatusChangedDomainEventHandler : INotificationHandler<BetCancelableStatusChangedDomainEvent>
    {
        private readonly ILogger<BetCancelableStatusChangedDomainEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IBettingIntegrationEventService _bettingIntgrationEventService;

        public BetCancelableStatusChangedDomainEventHandler(ILogger<BetCancelableStatusChangedDomainEventHandler> logger,
                                             IBetRepository repository,
                                             IBettingIntegrationEventService bettingIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _bettingIntgrationEventService = bettingIntegrationEventService;
        }

        public async Task Handle(BetCancelableStatusChangedDomainEvent betIsCanceledChangedEvent, CancellationToken cancellationToken)
        {
            var bet = await _repository.GetAsync(betIsCanceledChangedEvent.BetId);

            var betCancelableStatusChangedIntegrationEvent = new BetCancelableStatusChangedIntegrationEvent(bet.GamblerId, 
                                                                                        bet.Id, bet.IsCancelable);
            // Add a BetCancelableStatusChangedIntegrationEvent to queue to be published latet
            // (receiver is the Betting.SignalRHub service)
            await _bettingIntgrationEventService.AddAndSaveEventAsync(betCancelableStatusChangedIntegrationEvent);
        }
    }
}
