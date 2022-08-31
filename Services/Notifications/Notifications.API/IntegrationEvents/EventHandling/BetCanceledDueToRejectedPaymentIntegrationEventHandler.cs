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
    public class BetCanceledDueToRejectedPaymentIntegrationEventHandler : IIntegrationEventHandler<BetCanceledDueToRejectedPaymentIntegrationEvent>
    {
        private readonly ILogger<BetCanceledDueToRejectedPaymentIntegrationEventHandler> _logger;
        private readonly INotificationsRepository _repository;

        public BetCanceledDueToRejectedPaymentIntegrationEventHandler(ILogger<BetCanceledDueToRejectedPaymentIntegrationEventHandler> logger,
                                                                      INotificationsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(BetCanceledDueToRejectedPaymentIntegrationEvent @event)
        {
            // first, create the string with the Description of the Notification
            var sb = new StringBuilder();
            sb.AppendFormat("Payment of €{0} for bet with Id: {1} failed. " +
                            "Bet has been canceled automatically.", @event.WageredAmount, @event.BetId);

            // then create the Notification
            var notification = new Notification(@event.GamblerId, "Bet Payment Failed", sb.ToString());

            // finally, save the Notification to the DB
            _repository.AddNotification(notification);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
