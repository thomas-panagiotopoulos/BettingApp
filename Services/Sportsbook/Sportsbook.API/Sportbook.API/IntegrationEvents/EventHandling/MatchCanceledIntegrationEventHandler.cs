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
    public class MatchCanceledIntegrationEventHandler : IIntegrationEventHandler<MatchCanceledIntegrationEvent>
    {
        private readonly ILogger<MatchCanceledIntegrationEventHandler> _logger;
        private readonly ISportsbookRepository _repository;
        private readonly IEventBus _eventBus;

        public MatchCanceledIntegrationEventHandler(ILogger<MatchCanceledIntegrationEventHandler> logger,
                                                    ISportsbookRepository repository,
                                                    IEventBus eventBus)
        {
            _logger = logger;
            _repository = repository;
            _eventBus = eventBus;
        }
        public async Task Handle(MatchCanceledIntegrationEvent @event)
        {
            // Get the Match from DB using the repository
            var match = await _repository.GetMatchAsync(@event.MatchId);

            // Cancel Match (we use null-conditional operator to ensure that
            // no NullReferenceException will be thrown in case Match is not found and thus is null)
            match?.Cancel();

            // Save changes to the DB
            await _repository.UnitOfWork.SaveChangesAsync();

            // publish a SportsbookMatchCanceledIntegrationEvent and a SportsbookMatchResultOddOrBetableStatusChangedIntegrationEvent
            // for each MatchResult. (for the SignalR service, to notify clients for visual changes)
            if (match != null)
            {
                // We explicitely notify SignalR service that every one of the Match's PossiblePicks was disabled
                // since the Match got canceled
                foreach (var pp in match.PossiblePicks)
                {
                    var sportsbookMatchResultOddOrBetableStatusChangedIntegrationEvent =
                            new SportsbookMatchResultOddOrBetableStatusChangedIntegrationEvent(pp.MatchId, pp.MatchResultId,
                                                                MatchResult.From(pp.MatchResultId).AliasName, pp.Odd,
                                                                isBetable: false);
                    _eventBus.Publish(sportsbookMatchResultOddOrBetableStatusChangedIntegrationEvent);
                }
                // finally, we notify SignalR service that the Match got canceled
                var sportsbookMatchCanceledIntegrationEvent = new SportsbookMatchCanceledIntegrationEvent(match.Id);
                _eventBus.Publish(sportsbookMatchCanceledIntegrationEvent);
            }

        }
    }
}
