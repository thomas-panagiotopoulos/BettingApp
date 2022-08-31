using BettingApp.Services.MatchSimulation.API.Application.IntegrationEvents;
using BettingApp.Services.MatchSimulation.API.Application.IntegrationEvents.Events;
using BettingApp.Services.MatchSimulation.API.Extensions;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
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
    public class MatchCreatedDomainEventHandler : INotificationHandler<MatchCreatedDomainEvent>
    {
        private readonly ILogger<MatchCreatedDomainEventHandler> _logger;
        private readonly IMatchRepository _repository;
        private readonly IMatchSimulationIntegrationEventService _matchSimulationintegrationEventService;

        public MatchCreatedDomainEventHandler(ILogger<MatchCreatedDomainEventHandler> logger,
                                              IMatchRepository repository,
                                              IMatchSimulationIntegrationEventService matchSimulationintegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _matchSimulationintegrationEventService = matchSimulationintegrationEventService;
        }

        public async Task Handle(MatchCreatedDomainEvent matchCreatedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A MatchCreatedDomainEvent is currently being handled...");

            // Load the Match through the repository
            var match = await _repository.GetByIdAsync(matchCreatedEvent.MatchId);

            // create a MatchCreatedIntegrationEvent and schedule it for publishing through IntegrationEventService
            var matchCreatedIntegrationEvent = new MatchCreatedIntegrationEvent(match.ToMatchDTO());
            await _matchSimulationintegrationEventService.AddAndSaveEventAsync(matchCreatedIntegrationEvent);

            _logger.LogInformation("A MatchCreatedIntergrationEvent was added to queue to be published later.");
        }
    }
}
