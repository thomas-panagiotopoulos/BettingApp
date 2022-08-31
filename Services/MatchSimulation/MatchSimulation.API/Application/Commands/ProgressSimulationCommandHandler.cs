using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
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
    public class ProgressSimulationCommandHandler : IRequestHandler<ProgressSimulationCommand, Simulation>
    {
        private readonly ILogger<ProgressSimulationCommand> _logger;
        private readonly ISimulationRepository _simulationRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IClubRepository _clubRepository;

        public ProgressSimulationCommandHandler(ILogger<ProgressSimulationCommand> logger,
                                                ISimulationRepository simulationRepository,
                                                IMatchRepository matchRepository,
                                                IClubRepository clubRepository)
        {
            _logger = logger;
            _simulationRepository = simulationRepository;
            _matchRepository = matchRepository;
            _clubRepository = clubRepository;
        }
        public async Task<Simulation> Handle(ProgressSimulationCommand request, CancellationToken cancellationToken)
        {
            // load Simulation from the repository
            var simulation = await _simulationRepository.GetByIdAsync(request.SimulationId);

            // load related Match from the repository         
            var match = await _matchRepository.GetBySimulationIdAsync(request.SimulationId);

            // load related Clubs from the repository
            var homeClub = await _clubRepository.GetByIdAsync(match.HomeClubId);
            var awayClub = await _clubRepository.GetByIdAsync(match.AwayClubId);

            // progress Simulation through the provided class methods (this will add Domain Events to be published later)
            simulation.ProgressByOneMinute(homeClub, awayClub);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _simulationRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return simulation;
        }
    }
}
