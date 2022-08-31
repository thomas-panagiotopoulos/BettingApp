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
    public class TopUpDeniedByBankIntegrationEventHandler : IIntegrationEventHandler<TopUpDeniedByBankIntegrationEvent>
    {
        private readonly ILogger<TopUpDeniedByBankIntegrationEventHandler> _logger;
        private readonly IWalletsRepository _repository;
        private readonly IWalletsIntegrationEventService _walletsIntegrationEventService;

        public TopUpDeniedByBankIntegrationEventHandler(ILogger<TopUpDeniedByBankIntegrationEventHandler> logger,
                                                 IWalletsRepository repository,
                                                 IWalletsIntegrationEventService walletsIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _walletsIntegrationEventService = walletsIntegrationEventService;
        }

        public async Task Handle(TopUpDeniedByBankIntegrationEvent @event)
        {
            // first log the information
            _logger.LogInformation($"TopUp for User:{@event.GamblerId} with RequestId:{@event.RequestId} was denied by the bank.");

            // then publish a TopUpRequestFailedIntegrationEvent (for Notifications)
            var topUpRequestFailedIntegrationEvent = new TopUpRequestFailedIntegrationEvent(@event.GamblerId, @event.RequestId, @event.Amount);
            await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(topUpRequestFailedIntegrationEvent);
            await _walletsIntegrationEventService.PublishThroughEventBusAsync(topUpRequestFailedIntegrationEvent);
        }
    }
}
