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
    public class BetMarkedAsPaidIntegrationEventHandler : IIntegrationEventHandler<BetMarkedAsPaidIntegrationEvent>
    {
        private readonly ILogger<BetMarkedAsPaidIntegrationEventHandler> _logger;
        private readonly INotificationsRepository _repository;

        public BetMarkedAsPaidIntegrationEventHandler(ILogger<BetMarkedAsPaidIntegrationEventHandler> logger,
                                                      INotificationsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(BetMarkedAsPaidIntegrationEvent @event)
        {
            // first, create the string with the Description of the Notification
            var sb = new StringBuilder();
            sb.AppendFormat("Payment of €{0} for Bet with Id: {1} was successful.", @event.WageredAmount, @event.BetId);

            // then create the Notification
            var notification = new Notification(@event.GamblerId, "Bet Payment Succeeded", sb.ToString());

            // finally, save the Notification to the DB
            _repository.AddNotification(notification);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
