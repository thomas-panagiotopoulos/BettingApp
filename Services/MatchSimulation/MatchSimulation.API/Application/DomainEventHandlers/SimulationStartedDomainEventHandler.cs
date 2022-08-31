using BettingApp.Services.MatchSimulation.API.Application.Commands;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SimulationAggregate;
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
    public class SimulationStartedDomainEventHandler : INotificationHandler<SimulationStartedDomainEvent>
    {
        private readonly ILogger<SimulationStartedDomainEventHandler> _logger;
        private readonly IMediator _mediator;

        public SimulationStartedDomainEventHandler(ILogger<SimulationStartedDomainEventHandler> logger,
                                                   IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
            
        }
        public async Task Handle(SimulationStartedDomainEvent simulationStartedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A SimulationStartedDomainEvent is currently being handled...");

            // Send a command to Link related Clubs with Simulation
            var linkClubsCommand = new LinkClubsWithSimulationCommand(simulationStartedEvent.SimulationId);

            _logger.LogInformation("A LinkClubsWithSimulationCommand will be sent.");

            await _mediator.Send(linkClubsCommand);
        }
    }
}
