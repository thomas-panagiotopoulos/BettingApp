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
    public class BetMarkedAsPaidDomainEventHandler : INotificationHandler<BetMarkedAsPaidDomainEvent>
    {
        private readonly ILogger<BetMarkedAsPaidDomainEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IBettingIntegrationEventService _bettingIntegrationEventService;

        public BetMarkedAsPaidDomainEventHandler(ILogger<BetMarkedAsPaidDomainEventHandler> logger,
                                             IBetRepository repository,
                                             IBettingIntegrationEventService bettingIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _bettingIntegrationEventService = bettingIntegrationEventService;
        }

        public async Task Handle(BetMarkedAsPaidDomainEvent betPaidEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A BetMarkedAsPaidDomainEvent is currently being handled...");

            var bet = await _repository.GetAsync(betPaidEvent.BetId);

            // Add a BetMarkedAsPaidIntergrationEvent to the queue to be published later
            var betMarkedAsPaidIntegrationEvent = new BetMarkedAsPaidIntegrationEvent(bet.GamblerId, bet.Id, bet.WageredAmount);
            await _bettingIntegrationEventService.AddAndSaveEventAsync(betMarkedAsPaidIntegrationEvent);

            _logger.LogInformation("A BetMarkedAsPaidIntergrationEvent was added to queue to be published later.");
        }
    }
}
