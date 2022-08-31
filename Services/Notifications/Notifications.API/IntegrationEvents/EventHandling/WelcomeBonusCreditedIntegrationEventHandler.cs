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
    public class WelcomeBonusCreditedIntegrationEventHandler : IIntegrationEventHandler<WelcomeBonusCreditedIntegrationEvent>
    {
        private readonly ILogger<WelcomeBonusCreditedIntegrationEventHandler> _logger;
        private readonly INotificationsRepository _repository;

        public WelcomeBonusCreditedIntegrationEventHandler(ILogger<WelcomeBonusCreditedIntegrationEventHandler> logger,
                                                           INotificationsRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(WelcomeBonusCreditedIntegrationEvent @event)
        {
            // first, create the string with the Description of the Notification
            var sb = new StringBuilder();
            sb.AppendFormat("A 'Welcome Bonus' of €{0} has been credited to yout wallet. " +
                            "Your new wallet balance is €{1}.", @event.WelcomeBonusAmount, @event.NewBalance);

            // then create the Notification
            var notification = new Notification(@event.GamblerId, "Welcome Bonus Credited", sb.ToString());

            // finally, save the Notification to the DB
            _repository.AddNotification(notification);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
