using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Sportbook.API.IntegrationEvents.Events;
using BettingApp.Services.Sportbook.API.IntegrationEvents.Events.SignalRHub;
using BettingApp.Services.Sportbook.API.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.IntegrationEvents.EventHandling
{
    public class MatchResultOddOrStatusChangedIntegrationEventHandler : IIntegrationEventHandler<MatchResultOddOrStatusChangedIntegrationEvent>
    {
        private readonly ILogger<MatchResultOddOrStatusChangedIntegrationEventHandler> _logger;
        private readonly ISportsbookRepository _repository;
        private readonly IEventBus _eventBus;

        public MatchResultOddOrStatusChangedIntegrationEventHandler(
                                                ILogger<MatchResultOddOrStatusChangedIntegrationEventHandler> logger,
                                                ISportsbookRepository repository,
                                                IEventBus eventBus)
        {
            _logger = logger;
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task Handle(MatchResultOddOrStatusChangedIntegrationEvent @event)
        {

            // Get the Match from DB using the repository
            var match = await _repository.GetMatchAsync(@event.MatchId);

            // Update Match (we use null-conditional operator to ensure that
            // no NullReferenceException will be thrown in case Match is not found and thus is null)
            match?.UpdatePossiblePick(@event.MatchResultId, @event.NewOdd, @event.IsDisabled);

            // Save changes to the DB
            await _repository.UnitOfWork.SaveChangesAsync();

            // publish a SportsbookMatchResultOddOrBetableStatusIntegrationEvent
            // (for the SignalR service, to notify clients for visual changes)
            if(match != null)
            {
                var possiblePick = match.FindPossiblePick(@event.MatchResultId);
                var sportsbookMatchResultOddOrBetableStatusChangedIntegrationEvent =
                        new SportsbookMatchResultOddOrBetableStatusChangedIntegrationEvent(match.Id, possiblePick.MatchResultId,
                                                                    MatchResult.From(possiblePick.MatchResultId).AliasName,
                                                                    possiblePick.Odd, possiblePick.IsBetable);

                _eventBus.Publish(sportsbookMatchResultOddOrBetableStatusChangedIntegrationEvent);
            }
        }
    }
}
