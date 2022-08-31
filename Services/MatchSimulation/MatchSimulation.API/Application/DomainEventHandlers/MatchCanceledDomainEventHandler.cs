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
    public class MatchCanceledDomainEventHandler : INotificationHandler<MatchCanceledDomainEvent>
    {
        private readonly ILogger<MatchCanceledDomainEventHandler> _logger;
        private readonly IMatchRepository _repository;
        private readonly IMatchSimulationIntegrationEventService _matchSimulationintegrationEventService;

        public MatchCanceledDomainEventHandler(ILogger<MatchCanceledDomainEventHandler> logger,
                                               IMatchRepository repository,
                                               IMatchSimulationIntegrationEventService matchSimulationintegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _matchSimulationintegrationEventService = matchSimulationintegrationEventService;
        }
        public async Task Handle(MatchCanceledDomainEvent matchCanceledEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A MatchCanceledDomainEvent is currently being handled...");

            // create a MatchCanceledIntegrationEvent and schedule it for publishing through the IntegrationEventService
            var matchCanceledIntegrationEvent = new MatchCanceledIntegrationEvent(matchCanceledEvent.MatchId);
            await _matchSimulationintegrationEventService.AddAndSaveEventAsync(matchCanceledIntegrationEvent);

            _logger.LogInformation("A MatchCanceledIntegrationEvent was added to queue to be published later.");
        }
    }
}
