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
    public class PossiblePickOddOrStatusChangedDomainEventHandler : INotificationHandler<PossiblePickOddOrStatusChangedDomainEvent>
    {
        private readonly ILogger<PossiblePickOddOrStatusChangedDomainEventHandler> _logger;
        private readonly IMatchSimulationIntegrationEventService _matchSimulationIntegrationEventService;

        public PossiblePickOddOrStatusChangedDomainEventHandler(
                                                    ILogger<PossiblePickOddOrStatusChangedDomainEventHandler> logger,
                                                    IMatchSimulationIntegrationEventService matchSimulationIntegrationEventService)
        {
            _logger = logger;
            _matchSimulationIntegrationEventService = matchSimulationIntegrationEventService;
        }

        public async Task Handle(PossiblePickOddOrStatusChangedDomainEvent possiblePicksChangedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A PossiblePickOddOrStatusChangedDomainEvent is currently being handled...");

            // create a MatchResultOddOrStatusChangedIntegrationEvent and schedule it for publishing
            // through the IntegrationEventService
            var matchResultOddOrStatusChangedIntegrationEvent = new MatchResultOddOrStatusChangedIntegrationEvent(
                                                                                possiblePicksChangedEvent.MatchId,
                                                                                possiblePicksChangedEvent.MatchResultId,
                                                                                possiblePicksChangedEvent.NewOdd,
                                                                                possiblePicksChangedEvent.IsDisabled);
            await _matchSimulationIntegrationEventService.AddAndSaveEventAsync(matchResultOddOrStatusChangedIntegrationEvent);

            _logger.LogInformation("A MatchResultOddOrStatusChangedIntegrationEvent was added to queue to be published later.");
        }
    }
}
