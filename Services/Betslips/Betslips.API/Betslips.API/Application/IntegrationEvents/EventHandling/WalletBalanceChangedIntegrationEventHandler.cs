using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Betslips.API.Application.Commands;
using BettingApp.Services.Betslips.API.Application.IntegrationEvents.Events;
using BettingApp.Services.Betslips.Domain.AggregatesModel.WalletAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.IntegrationEvents.EventHandling
{
    public class WalletBalanceChangedIntegrationEventHandler
        : IIntegrationEventHandler<WalletBalanceChangedIntegrationEvent>
    {
        private readonly ILogger<WalletBalanceChangedIntegrationEventHandler> _logger;
        private readonly IWalletRepository _repository;
        private readonly IMediator _mediator;

        public WalletBalanceChangedIntegrationEventHandler(ILogger<WalletBalanceChangedIntegrationEventHandler> logger,
                                                               IWalletRepository repository,
                                                               IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(WalletBalanceChangedIntegrationEvent @event)
        {
            var exists = _repository.ExistsWithGamblerId(@event.GamblerId);

            if (!exists)
            {
                await _mediator.Send(new CreateWalletCommand(@event.GamblerId));
            }

            var command = new UpdateWalletBalanceCommand(@event.GamblerId, @event.NewBalance, @event.OldBalance);

            await _mediator.Send(command);
        }
    }
}
