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
    public class BetCanceledIntegrationEventHandler : IIntegrationEventHandler<BetCanceledIntegrationEvent>
    {
        private readonly ILogger<BetCanceledIntegrationEventHandler> _logger;
        private readonly INotificationsRepository _repository;

        public BetCanceledIntegrationEventHandler(ILogger<BetCanceledIntegrationEventHandler> logger,
                                                  INotificationsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(BetCanceledIntegrationEvent @event)
        {
            // first, create the string with the Description of the Notification
            var sb = new StringBuilder();
            sb.AppendFormat("Bet with Id: {0} has been canceled. " +
                            "Wagered amount of €{1} will be refunded to your wallet soon.", @event.BetId, @event.WageredAmount);

            // then create the Notification
            var notification = new Notification(@event.GamblerId, "Bet Canceled", sb.ToString());

            // finally, save the Notification to the DB
            _repository.AddNotification(notification);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
