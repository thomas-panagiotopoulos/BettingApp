using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Betslips.API.Application.Commands;
using BettingApp.Services.Betslips.API.Application.IntegrationEvents.Events;
using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.IntegrationEvents.EventHandling
{
    public class BetMarkedAsPaidIntegrationEventHandler : IIntegrationEventHandler<BetMarkedAsPaidIntegrationEvent>
    {
        private readonly ILogger<BetMarkedAsPaidIntegrationEventHandler> _logger;
        private readonly IBetslipRepository _repository;
        private readonly IMediator _mediator;

        public BetMarkedAsPaidIntegrationEventHandler(ILogger<BetMarkedAsPaidIntegrationEventHandler> logger,
                                                      IBetslipRepository repository,
                                                      IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(BetMarkedAsPaidIntegrationEvent @event)
        {
            var exists = _repository.ExistsWithGamblerId(@event.GamblerId);

            if (!exists) 
                return;

            var command = new ClearBetslipCommand(@event.GamblerId);

            await _mediator.Send(command);
        }
    }
}
