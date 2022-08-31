using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SimulationAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.Commands
{
    public class CreateSimulationCommandHandler : IRequestHandler<CreateSimulationCommand, Simulation>
    {
        private readonly ILogger<CreateSimulationCommandHandler> _logger;
        private readonly ISimulationRepository _repository;

        public CreateSimulationCommandHandler(ILogger<CreateSimulationCommandHandler> logger,
                                              ISimulationRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Simulation> Handle(CreateSimulationCommand request, CancellationToken cancellationToken)
        {
            // create the Simulation
            var simulation = new Simulation(request.MatchId, request.HomeClub, request.AwayClub, request.LeagueId, request.KickoffDateTime);

            // add the Simulation to the DB through the repository
            var addedSimulation = _repository.Add(simulation);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return addedSimulation;
        }
    }
}
