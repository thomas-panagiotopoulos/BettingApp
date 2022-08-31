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
    public class ApplyTopUpSucceededIntegrationEventHandler : IIntegrationEventHandler<ApplyTopUpSucceededIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<ApplyTopUpSucceededIntegrationEventHandler> _logger;

        public ApplyTopUpSucceededIntegrationEventHandler(IEventBus eventBus,
                                                          ILogger<ApplyTopUpSucceededIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task Handle(ApplyTopUpSucceededIntegrationEvent @event)
        {
            // In a real case senario, here we'd communicate with a payment gateway, in order to finalize the
            // original payment that was requested for the TopUp transaction that was completed succesfully.
            // Instead, here we will only log the information

            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            _logger.LogInformation($"TopUp transaction with RequestId: {@event.RequestId} applied succesfully. " +
                                    "Payment will be finalized against the payment gateway.");

            await Task.CompletedTask;
        }
    }
}
