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
    public class BetRemovedDueToUnpaidCompletionIntegrationEventHandler : IIntegrationEventHandler<BetRemovedDueToUnpaidCompletionIntegrationEvent>
    {
        private readonly ILogger<BetRemovedDueToUnpaidCompletionIntegrationEventHandler> _logger;
        private readonly INotificationsRepository _repository;

        public BetRemovedDueToUnpaidCompletionIntegrationEventHandler(ILogger<BetRemovedDueToUnpaidCompletionIntegrationEventHandler> logger,
                                                                      INotificationsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(BetRemovedDueToUnpaidCompletionIntegrationEvent @event)
        {
            // first, create the string with the Description of the Notification
            var sb = new StringBuilder();
            sb.AppendFormat("Bet with Id: {0} has been removed from your account, because it was completed " +
                            "but no linked payment information was found.", @event.BetId);

            // then create the Notification
            var notification = new Notification(@event.GamblerId, "Bet Without Payment Removed", sb.ToString());

            // finally, save the Notification to the DB
            _repository.AddNotification(notification);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
