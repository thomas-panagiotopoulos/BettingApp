using BettingApp.Services.MatchSimulation.API.Application.Commands;
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
    public class SimulationCreatedDomainEventHandler : INotificationHandler<SimulationCreatedDomainEvent>
    {
        private readonly ILogger<SimulationCreatedDomainEventHandler> _logger;
        private readonly IMediator _mediator;

        public SimulationCreatedDomainEventHandler(ILogger<SimulationCreatedDomainEventHandler> logger,
                                                   IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task Handle(SimulationCreatedDomainEvent simulationCreatedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A SimulationCreatedDomainEvent is currently being handled...");

            var createMatchCommand = new CreateMatchCommand(simulationCreatedEvent.MatchId, simulationCreatedEvent.SimulationId,
                                                simulationCreatedEvent.HomeClub, simulationCreatedEvent.AwayClub,
                                                simulationCreatedEvent.LeagueId, simulationCreatedEvent.KickoffDateTime,
                                                simulationCreatedEvent.PossiblePicks);

            _logger.LogInformation("A CreateMatchCommand will be sent.");

            var result = await _mediator.Send(createMatchCommand);

        }
    }
}
