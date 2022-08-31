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
    public class BetWageredAmountRefundedIntegrationEventHandler : IIntegrationEventHandler<BetWageredAmountRefundedIntegrationEvent>
    {
        private readonly ILogger<BetWageredAmountRefundedIntegrationEventHandler> _logger;
        private readonly INotificationsRepository _repository;

        public BetWageredAmountRefundedIntegrationEventHandler(ILogger<BetWageredAmountRefundedIntegrationEventHandler> logger,
                                                               INotificationsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(BetWageredAmountRefundedIntegrationEvent @event)
        {
            // first, create the string with the Description of the Notification
            var sb = new StringBuilder();
            sb.AppendFormat("Wagered amount of €{0} for bet with Id: {1} has been refunded to your wallet. " +
                            "Your new wallet balance is €{2}.", @event.WageredAmount, @event.BetId, @event.NewBalance);

            // then create the Notification
            var notification = new Notification(@event.GamblerId, "Bet Wagered Amount Refunded", sb.ToString());

            // finally, save the Notification to the DB
            _repository.AddNotification(notification);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
