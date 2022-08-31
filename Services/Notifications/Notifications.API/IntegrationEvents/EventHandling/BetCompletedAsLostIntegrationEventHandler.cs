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
    public class BetCompletedAsLostIntegrationEventHandler : IIntegrationEventHandler<BetCompletedAsLostIntegrationEvent>
    {
        private readonly ILogger<BetCompletedAsLostIntegrationEventHandler> _logger;
        private readonly INotificationsRepository _repository;

        public BetCompletedAsLostIntegrationEventHandler(ILogger<BetCompletedAsLostIntegrationEventHandler> logger,
                                                         INotificationsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(BetCompletedAsLostIntegrationEvent @event)
        {
            // first, create the string with the Description of the Notification
            var sb = new StringBuilder();
            sb.AppendFormat("Bet with Id: {0} has been completed as 'Lost'.", @event.BetId);

            // then create the Notification
            var notification = new Notification(@event.GamblerId, "Bet Completed As Lost", sb.ToString());

            // finally, save the Notification to the DB
            _repository.AddNotification(notification);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
