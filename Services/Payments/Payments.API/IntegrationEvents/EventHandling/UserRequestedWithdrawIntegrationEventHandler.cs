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
    public class UserRequestedWithdrawIntegrationEventHandler : IIntegrationEventHandler<UserRequestedWithdrawIntegrationEvent>
    {
        private readonly IEventBus _eventBus;
        private readonly PaymentsSettings _settings;
        private readonly ILogger<UserRequestedWithdrawIntegrationEventHandler> _logger;

        public UserRequestedWithdrawIntegrationEventHandler(IEventBus eventBus,
                                                        IOptionsSnapshot<PaymentsSettings> settings,
                                                        ILogger<UserRequestedWithdrawIntegrationEventHandler> logger)
        {
            _eventBus = eventBus;
            _settings = settings.Value;
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));

            _logger.LogTrace("PaymentsSettings: {@PaymentsSettings}", _settings);
        }

        public async Task Handle(UserRequestedWithdrawIntegrationEvent @event)
        {
            // In a real case senario, here we'd be performing a transfer against any payment gateway,
            // but instead of a real transfer, we're just simulating a 'mock' transfer where we take the
            // environment variable to simulate the transfer. The transfer can either be accepted or denied.

            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            IntegrationEvent withdrawTransferIntegrationEvent;

            if (_settings.WithdrawAccepted)
            {
                withdrawTransferIntegrationEvent = new WithdrawAcceptedByBankIntegrationEvent(@event.GamblerId, 
                                                                                              @event.Amount,
                                                                                              @event.RequestId);
            }
            else
            {
                withdrawTransferIntegrationEvent = new WithdrawDeniedByBankIntegrationEvent(@event.GamblerId, 
                                                                                            @event.Amount,
                                                                                            @event.RequestId);
            }

            _logger.LogInformation("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", withdrawTransferIntegrationEvent.Id, Program.AppName, withdrawTransferIntegrationEvent);

            _eventBus.Publish(withdrawTransferIntegrationEvent);

            await Task.CompletedTask;
        }
    }
}
