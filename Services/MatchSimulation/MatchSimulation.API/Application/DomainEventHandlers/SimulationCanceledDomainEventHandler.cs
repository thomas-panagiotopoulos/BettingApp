using BettingApp.Services.MatchSimulation.API.Application.Commands;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
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
    public class SimulationCanceledDomainEventHandler : INotificationHandler<SimulationCanceledDomainEvent>
    {
        private readonly ILogger<SimulationCanceledDomainEventHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IMatchRepository _matchRepository;
        private readonly IClubRepository _clubRepository;

        public SimulationCanceledDomainEventHandler(ILogger<SimulationCanceledDomainEventHandler> logger,
                                                    IMediator mediator,
                                                    IMatchRepository matchRepository,
                                                    IClubRepository clubRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _matchRepository = matchRepository;
            _clubRepository = clubRepository;
        }

        public async Task Handle(SimulationCanceledDomainEvent simulationCanceledEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A SimulationCanceledDomainEvent is currently being handled...");

            // Send a command to Cancel the related Match
            var cancelMatchCommand = new CancelMatchCommand(simulationCanceledEvent.MatchId);

            _logger.LogInformation("A CancelMatchCommand will be sent.");

            await _mediator.Send(cancelMatchCommand);

            // Send another command to Unlink related Clubs, if Simulation had Ongoing status before cancelation
            if (simulationCanceledEvent.SimulationWasOngoing)
            {
                var unlinkClubsCommand = new UnlinkClubsFromSimulationCommand(simulationCanceledEvent.SimulationId);

                _logger.LogInformation("An UnlinkClubsFromSimulationCommand will be sent.");

                await _mediator.Send(unlinkClubsCommand);
            }

        }
    }
}
