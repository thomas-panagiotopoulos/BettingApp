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
    public class SimulationCompletedDomainEventHandler : INotificationHandler<SimulationCompletedDomainEvent>
    {
        private readonly ILogger<SimulationCompletedDomainEventHandler> _logger;
        private readonly IMediator _mediator;

        public SimulationCompletedDomainEventHandler(ILogger<SimulationCompletedDomainEventHandler> logger,
                                                     IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        public async Task Handle(SimulationCompletedDomainEvent simulationCompletedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A SimulationCompletedDomainEvent is currently being handled...");

            // Send an UpdateClubsFormCommand to update related Clubs' Form after Simulation completion
            var updateClubsFormCommand = new UpdateClubsFormCommand(simulationCompletedEvent.SimulationId);

            _logger.LogInformation("An UpdateClubsFormCommand will be sent.");

            await _mediator.Send(updateClubsFormCommand);

            // Send an UnlinkClubsFromSimulationCommand to unlink related Clubs from Simulation after completion
            // note: it's important that UnlinkClubsFromSimulationCommand is sent AFTER the UpdateClubsFormCommand,
            // otherwise the latter command will fail.
            var unlinkClubsCommand = new UnlinkClubsFromSimulationCommand(simulationCompletedEvent.SimulationId);

            _logger.LogInformation("An UnlinkClubsFromSimulationCommand will be sent.");

            await _mediator.Send(unlinkClubsCommand);
        }
    }
}
