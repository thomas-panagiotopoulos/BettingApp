using BettingApp.Services.MatchSimulation.API.Application.IntegrationEvents;
using BettingApp.Services.MatchSimulation.API.Application.IntegrationEvents.Events;
using BettingApp.Services.MatchSimulation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.DomainEventHandlers
{
    public class MatchCurrentMinuteChangedDomainEventHandler : INotificationHandler<MatchCurrentMinuteChangedDomainEvent>
    {
        private readonly ILogger<MatchCurrentMinuteChangedDomainEventHandler> _logger;
        private readonly IMatchSimulationIntegrationEventService _matchSimulationIntegrationEventService;

        public MatchCurrentMinuteChangedDomainEventHandler(ILogger<MatchCurrentMinuteChangedDomainEventHandler> logger,
                                                           IMatchSimulationIntegrationEventService matchSimulationIntegrationEventService)
        {
            _logger = logger;
            _matchSimulationIntegrationEventService = matchSimulationIntegrationEventService;
        }

        public async Task Handle(MatchCurrentMinuteChangedDomainEvent matchCurrentMinuteChangedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A MatchCurrentMinuteChangedDomainEvent is currently being handled...");

            // create a MatchCurrentMinuteChangedIntegrationEvent and schedule it for publishing
            // through the IntegrationEventService
            var matchCurrentMinuteChangedIntegrationEvent = new MatchCurrentMinuteChangedIntegrationEvent(
                                                                    matchCurrentMinuteChangedEvent.MatchId, 
                                                                    matchCurrentMinuteChangedEvent.NewCurrentMinute);
            await _matchSimulationIntegrationEventService.AddAndSaveEventAsync(matchCurrentMinuteChangedIntegrationEvent);

            _logger.LogInformation("A MatchCurrentMinuteChangedIntegrationEvent was added to queue to be published later.");
        }
    }
}
