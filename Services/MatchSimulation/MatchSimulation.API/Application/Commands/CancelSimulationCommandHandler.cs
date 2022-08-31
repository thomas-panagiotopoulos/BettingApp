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
    public class CancelSimulationCommandHandler : IRequestHandler<CancelSimulationCommand, Simulation>
    {
        private readonly ISimulationRepository _repository;

        public CancelSimulationCommandHandler(ILogger<CancelSimulationCommandHandler> logger,
                                              ISimulationRepository repository)
        {
            _repository = repository;
        }

        public async Task<Simulation> Handle(CancelSimulationCommand request, CancellationToken cancellationToken)
        {
            // Load Simulation from the repository
            var simulation = await _repository.GetByIdAsync(request.SimulationId);

            // Cancel the Simulation through the provided class method (this interally add Domain Events)
            simulation.Cancel();

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return simulation;
        }
    }
}
