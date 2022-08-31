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
    public class BetDetailsChangedIntegrationEventHandler : IIntegrationEventHandler<BetDetailsChangedIntegrationEvent>
    {
        private readonly ILogger<BetDetailsChangedIntegrationEventHandler> _logger;
        private readonly INotificationsRepository _repository;

        public BetDetailsChangedIntegrationEventHandler(ILogger<BetDetailsChangedIntegrationEventHandler> logger,
                                                         INotificationsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(BetDetailsChangedIntegrationEvent @event)
        {
            // note: this IntegrationEvent contains various info that changed on the related Bet,
            // but we only create a Notification about the change on TotalOdd.

            // first, create the string with the Description of the Notification
            var sb = new StringBuilder();
            sb.AppendFormat("Total odd of bet with Id: {0} has changed from {1} to {2}, " +
                            "due to a match being canceled.", @event.BetId, @event.OldTotalOdd, @event.NewTotalOdd);

            // then create the Notification
            var notification = new Notification(@event.GamblerId, "Bet Total Odd Changed", sb.ToString());

            // finally, save the Notification to the DB
            _repository.AddNotification(notification);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
