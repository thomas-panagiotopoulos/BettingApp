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
    public class WithdrawRequestSucceededIntegrationEventHandler : IIntegrationEventHandler<WithdrawRequestSucceededIntegrationEvent>
    {
        private readonly ILogger<WithdrawRequestSucceededIntegrationEventHandler> _logger;
        private readonly INotificationsRepository _repository;

        public WithdrawRequestSucceededIntegrationEventHandler(ILogger<WithdrawRequestSucceededIntegrationEventHandler> logger,
                                                               INotificationsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(WithdrawRequestSucceededIntegrationEvent @event)
        {
            // first, create the string with the Description of the Notification
            var sb = new StringBuilder();
            sb.AppendFormat("Your wallet withdraw request of €{0} was succesful. " +
                            "Your new wallet balance is €{1}.\n" +
                            "(Reference Id: {2})", @event.Amount, @event.NewBalance, @event.RequestId);

            // then create the Notification
            var notification = new Notification(@event.GamblerId, "Withdraw Request Succeeded", sb.ToString());

            // finally, save the Notification to the DB
            _repository.AddNotification(notification);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
