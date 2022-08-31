using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Notifications.API.IntegrationEvents.Events;
using BettingApp.Services.Notifications.API.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.Notifications.API.IntegrationEvents.EventHandling
{
    public class WithdrawRequestFailedIntegrationEventHandler : IIntegrationEventHandler<WithdrawRequestFailedIntegrationEvent>
    {
        private readonly ILogger<WithdrawRequestFailedIntegrationEventHandler> _logger;
        private readonly INotificationsRepository _repository;

        public WithdrawRequestFailedIntegrationEventHandler(ILogger<WithdrawRequestFailedIntegrationEventHandler> logger,
                                                            INotificationsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(WithdrawRequestFailedIntegrationEvent @event)
        {
            // first, create the string with the Description of the Notification
            var sb = new StringBuilder();
            sb.AppendFormat("Your wallet withdraw request of €{0} failed. " +
                            "No money has been withdrawn from your wallet.\n" +
                            "(Reference Id: {1})", @event.Amount, @event.RequestId);

            // then create the Notification
            var notification = new Notification(@event.GamblerId, "Withdraw Request Failed", sb.ToString());

            // finally, save the Notification to the DB
            _repository.AddNotification(notification);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
