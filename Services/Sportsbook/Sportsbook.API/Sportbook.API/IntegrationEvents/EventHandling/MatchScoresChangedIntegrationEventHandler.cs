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
    public class MatchScoresChangedIntegrationEventHandler : IIntegrationEventHandler<MatchScoresChangedIntegrationEvent>
    {
        private readonly ILogger<MatchScoresChangedIntegrationEventHandler> _logger;
        private readonly ISportsbookRepository _repository;
        private readonly IEventBus _eventBus;

        public MatchScoresChangedIntegrationEventHandler(ILogger<MatchScoresChangedIntegrationEventHandler> logger,
                                                         ISportsbookRepository repository,
                                                         IEventBus eventBus)
        {
            _logger = logger;
            _repository = repository;
            _eventBus = eventBus;
        }

        public async Task Handle(MatchScoresChangedIntegrationEvent @event)
        {
            // Get the Match from DB using the repository
            var match = await _repository.GetMatchAsync(@event.MatchId);

            // Update Match's scores (we use null-conditional operator to ensure that
            // no NullReferenceException will be thrown in case Match is not found and thus is null)
            match?.UpdateScores(@event.NewHomeClubScore, @event.NewAwayClubScore);

            // Save changes to the DB
            await _repository.UnitOfWork.SaveChangesAsync();

            // publish a SportsbookMatchScoresChangedIntegrationEvent
            // (for the SignalR service, to notify clients for visual changes)
            if (match != null)
            {
                var sportsbookMatchScoresChangedIntegrationEvent = 
                                new SportsbookMatchScoresChangedIntegrationEvent(match.Id, match.HomeClubScore, 
                                                                                 match.AwayClubScore);

                _eventBus.Publish(sportsbookMatchScoresChangedIntegrationEvent);
            }
        }
    }
}
