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
    public class ApplyWithdrawFailedIntegrationEventHandler : IIntegrationEventHandler<ApplyWithdrawFailedIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<ApplyWithdrawFailedIntegrationEventHandler> _logger;

        public ApplyWithdrawFailedIntegrationEventHandler(IEventBus eventBus,
                                                          ILogger<ApplyWithdrawFailedIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        public async Task Handle(ApplyWithdrawFailedIntegrationEvent @event)
        {
            // In a real case senario, here we'd communicate with a payment gateway, in order to revoke the
            // original transfer that was requested for the Withdraw transaction that failed.
            // Instead, here we will only log the information

            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            _logger.LogInformation($"Withdraw transaction with RequestId: {@event.RequestId} failed. " +
                                    "Transfer will be revoked against the payment gateway.");

            await Task.CompletedTask;
        }
    }
}
