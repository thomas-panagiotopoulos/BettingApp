using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Payments.API.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;
using BettingApp.Services.Payments.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Payments.API.IntegrationEvents.EventHandling
{
    public class ApplyTopUpFailedIntegrationEventHandler : IIntegrationEventHandler<ApplyTopUpFailedIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<ApplyTopUpFailedIntegrationEventHandler> _logger;

        public ApplyTopUpFailedIntegrationEventHandler(IEventBus eventBus,
                                                       ILogger<ApplyTopUpFailedIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task Handle(ApplyTopUpFailedIntegrationEvent @event)
        {
            // In a real case senario, here we'd communicate with a payment gateway, in order to compensate the
            // original payment that was requested for the TopUp transaction that failed.
            // Instead, here we will only log the information

            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            _logger.LogInformation($"TopUp transaction with RequestId: {@event.RequestId} failed. " +
                                    "Payment will be compensated against the payment gateway.");

            await Task.CompletedTask;
        }
    }
}
