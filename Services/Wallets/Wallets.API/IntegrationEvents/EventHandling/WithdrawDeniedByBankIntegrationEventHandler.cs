using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Wallets.API.IntegrationEvents.Events;
using BettingApp.Services.Wallets.API.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.IntegrationEvents.EventHandling
{
    public class WithdrawDeniedByBankIntegrationEventHandler : IIntegrationEventHandler<WithdrawDeniedByBankIntegrationEvent>
    {
        private readonly ILogger<WithdrawDeniedByBankIntegrationEventHandler> _logger;
        private readonly IWalletsRepository _repository;
        private readonly IWalletsIntegrationEventService _walletsIntegrationEventService;

        public WithdrawDeniedByBankIntegrationEventHandler(ILogger<WithdrawDeniedByBankIntegrationEventHandler> logger,
                                                 IWalletsRepository repository,
                                                 IWalletsIntegrationEventService walletsIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _walletsIntegrationEventService = walletsIntegrationEventService;
        }

        public async Task Handle(WithdrawDeniedByBankIntegrationEvent @event)
        {
            // first log the information
            _logger.LogInformation($"Withdraw for User:{@event.GamblerId} with RequestId:{@event.RequestId} was denied by the bank.");

            // then publish a WithdrawRequestFailedIntegrationEvent (for Notifications)
            var withdrawRequestFailedIntegrationEvent = new WithdrawRequestFailedIntegrationEvent(@event.GamblerId, @event.RequestId, @event.Amount);
            await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(withdrawRequestFailedIntegrationEvent);
            await _walletsIntegrationEventService.PublishThroughEventBusAsync(withdrawRequestFailedIntegrationEvent);
        }
    }
}
