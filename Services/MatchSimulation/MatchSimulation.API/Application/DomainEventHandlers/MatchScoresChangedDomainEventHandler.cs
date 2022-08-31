using BettingApp.Services.MatchSimulation.API.Application.IntegrationEvents;
using BettingApp.Services.MatchSimulation.API.Application.IntegrationEvents.Events;
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
    public class MatchScoresChangedDomainEventHandler : INotificationHandler<MatchScoresChangedDomainEvent>
    {
        private readonly ILogger<MatchScoresChangedDomainEventHandler> _logger;
        private readonly IMatchRepository _repository;
        private readonly IMatchSimulationIntegrationEventService _matchSimulationIntegrationEventService;

        public MatchScoresChangedDomainEventHandler(ILogger<MatchScoresChangedDomainEventHandler> logger,
                                                    IMatchRepository repository,
                                                    IMatchSimulationIntegrationEventService matchSimulationIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _matchSimulationIntegrationEventService = matchSimulationIntegrationEventService;
        }

        public async Task Handle(MatchScoresChangedDomainEvent matchScoresChangedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A MatchScoresChangedDomainEvent is currently being handled...");

            // create a MatchScoresChangedIntegrationEvent and schedule it for publishing though the IntegrationEventService
            var matchScoresChangedIntegrationEvent = new MatchScoresChangedIntegrationEvent(
                                                                            matchScoresChangedEvent.MatchId,
                                                                            matchScoresChangedEvent.NewHomeClubScore,
                                                                            matchScoresChangedEvent.NewAwayClubScore);
            await _matchSimulationIntegrationEventService.AddAndSaveEventAsync(matchScoresChangedIntegrationEvent);

            _logger.LogInformation("A MatchScoresChangedIntegrationEvent was added to queue to be published later.");
        }
    }
}
