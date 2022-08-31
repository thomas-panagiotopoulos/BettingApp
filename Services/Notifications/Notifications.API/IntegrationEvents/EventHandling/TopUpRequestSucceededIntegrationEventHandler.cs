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
    public class TopUpRequestSucceededIntegrationEventHandler : IIntegrationEventHandler<TopUpRequestSucceededIntegrationEvent>
    {
        private readonly ILogger<TopUpRequestSucceededIntegrationEventHandler> _logger;
        private readonly INotificationsRepository _repository;

        public TopUpRequestSucceededIntegrationEventHandler(ILogger<TopUpRequestSucceededIntegrationEventHandler> logger,
                                                        INotificationsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(TopUpRequestSucceededIntegrationEvent @event)
        {
            // first, create the string with the Description of the Notification
            var sb = new StringBuilder();
            sb.AppendFormat("Your wallet top up request of €{0} was successful. " +
                            "Your new wallet balance is €{1}.\n" +
                            "(Reference Id: {2})", @event.Amount, @event.NewBalance, @event.RequestId);

            // then create the Notification
            var notification = new Notification(@event.GamblerId, "Top Up Request Succeeded", sb.ToString());

            // finally, save the Notification to the DB
            _repository.AddNotification(notification);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
