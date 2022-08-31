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
    public class ApplyWithdrawSucceededIntegrationEventHandler : IIntegrationEventHandler<ApplyWithdrawSucceededIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<ApplyWithdrawSucceededIntegrationEventHandler> _logger;

        public ApplyWithdrawSucceededIntegrationEventHandler(IEventBus eventBus,
                                                          ILogger<ApplyWithdrawSucceededIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task Handle(ApplyWithdrawSucceededIntegrationEvent @event)
        {
            // In a real case senario, here we'd communicate with a payment gateway, in order to finalize the
            // original transfer that was requested for the Withdraw transaction that was completed succesfully.
            // Instead, here we will only log the information

            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            _logger.LogInformation($"Withdraw transaction with RequestId: {@event.RequestId} applied succesfully. " +
                                    "Transfer will be finalized against the payment gateway.");

            await Task.CompletedTask;
        }
    }
}
