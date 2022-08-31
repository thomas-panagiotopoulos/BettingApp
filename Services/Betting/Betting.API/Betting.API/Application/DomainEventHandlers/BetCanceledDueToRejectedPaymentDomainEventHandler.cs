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
    public class BetCanceledDueToRejectedPaymentDomainEventHandler
                    : INotificationHandler<BetCanceledDueToRejectedPaymentDomainEvent>
    {
        private readonly ILogger<BetCanceledDueToRejectedPaymentDomainEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IBettingIntegrationEventService _bettingIntegrationEventService;

        public BetCanceledDueToRejectedPaymentDomainEventHandler(ILogger<BetCanceledDueToRejectedPaymentDomainEventHandler> logger,
                                                                IBetRepository repository,
                                                                IBettingIntegrationEventService bettingIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _bettingIntegrationEventService = bettingIntegrationEventService;
        }

        public async Task Handle(BetCanceledDueToRejectedPaymentDomainEvent betCanceledEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A BetCanceledDueToRejectedPaymentDomainEvent is currently being handled...");

            var bet = await _repository.GetAsync(betCanceledEvent.BetId);

            // Add a BetCanceledDueToRejectedPaymentIntergrationEvent to the queue to be published later
            var betCanceledDueToRejectedPaymentIntegrationEvent = new BetCanceledDueToRejectedPaymentIntegrationEvent(bet.Id, bet.GamblerId, bet.WageredAmount);
            await _bettingIntegrationEventService.AddAndSaveEventAsync(betCanceledDueToRejectedPaymentIntegrationEvent);

            _logger.LogInformation("A BetCanceledDueToRejectedPaymentIntergrationEvent was added to queue to be published later.");
        }
    }
}
