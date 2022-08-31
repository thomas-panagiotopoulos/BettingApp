using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.BuildingBlocks.EventBus.Events;
using BettingApp.Services.Payments.API.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BettingApp.Services.Payments.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Payments.API.IntegrationEvents.EventHandling
{
    public class UserRequestedTopUpIntegrationEventHandler : IIntegrationEventHandler<UserRequestedTopUpIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly PaymentsSettings _settings;
        private readonly ILogger<UserRequestedTopUpIntegrationEventHandler> _logger;

        public UserRequestedTopUpIntegrationEventHandler(IEventBus eventBus,
                                                        IOptionsSnapshot<PaymentsSettings> settings,
                                                        ILogger<UserRequestedTopUpIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _settings = settings.Value;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));

            _logger.LogTrace("PaymentsSettings: {@PaymentsSettings}", _settings);
        }

        public async Task Handle(UserRequestedTopUpIntegrationEvent @event)
        {
            // In a real case senario, here we'd be performing a payment against any payment gateway,
            // but instead of a real payment, we're just simulating a 'mock' payment where we take the
            // environment variable to simulate the payment. The payment can either be accepted or denied.

            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            IntegrationEvent topUpPaymentIntegrationEvent;

            if (_settings.TopUpAccepted)
            {
                topUpPaymentIntegrationEvent = new TopUpAcceptedByBankIntegrationEvent(@event.GamblerId, @event.Amount,
                                                                                       @event.RequestId);
            }
            else
            {
                topUpPaymentIntegrationEvent = new TopUpDeniedByBankIntegrationEvent(@event.GamblerId, @event.Amount,
                                                                                       @event.RequestId);
            }

            _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", topUpPaymentIntegrationEvent.Id, Program.AppName, topUpPaymentIntegrationEvent);

            _eventBus.Publish(topUpPaymentIntegrationEvent);

            await Task.CompletedTask;
        }
    }
}
