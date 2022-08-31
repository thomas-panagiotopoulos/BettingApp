using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Betting.API.Application.Commands;
using BettingApp.Services.Betting.API.Application.IntegrationEvents.Events;
using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.IntegrationEvents.EventHandling
{
    public class BetPaymentSucceededIntegrationEventHandler : IIntegrationEventHandler<BetPaymentSucceededIntegrationEvent>
    {
        private readonly ILogger<BetPaymentSucceededIntegrationEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IMediator _mediator;

        public BetPaymentSucceededIntegrationEventHandler(ILogger<BetPaymentSucceededIntegrationEventHandler> logger,
                                                          IBetRepository repository,
                                                          IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(BetPaymentSucceededIntegrationEvent @event)
        {
            // check if Bet exists
            var exists = _repository.Exists(@event.BetId);

            if (!exists)
            {
                _logger.LogInformation($"Bet with Id:{@event.BetId} was not found.");
                return;
            }

            // check if Bet belongs to correct Gambler (optional?)
            var bet = await _repository.GetAsync(@event.BetId);

            if (!bet.GamblerId.Equals(@event.GamblerId))
            {
                _logger.LogInformation($"Bet with Id:{bet.Id} does not belong to Gambler with Id:{@event.GamblerId}.");
                return;
            }

            // If everything is ok, procceed by sending the command
            var markBetAsPaidCommand = new MarkBetAsPaidCommand(@event.BetId);

            await _mediator.Send(markBetAsPaidCommand);
        }
    }
}
