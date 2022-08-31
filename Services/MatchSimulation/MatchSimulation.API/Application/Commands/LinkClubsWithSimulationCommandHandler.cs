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
    public class LinkClubsWithSimulationCommandHandler : IRequestHandler<LinkClubsWithSimulationCommand, IEnumerable<Club>>
    {
        private readonly ILogger<LinkClubsWithSimulationCommandHandler> _logger;
        private readonly ISimulationRepository _simulationRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IClubRepository _clubRepository;

        public LinkClubsWithSimulationCommandHandler(ILogger<LinkClubsWithSimulationCommandHandler> logger,
                                                   ISimulationRepository simulationRepository,
                                                   IMatchRepository matchRepository,
                                                   IClubRepository clubRepository)
        {
            _logger = logger;
            _simulationRepository = simulationRepository;
            _matchRepository = matchRepository;
            _clubRepository = clubRepository;
        }

        public async Task<IEnumerable<Club>> Handle(LinkClubsWithSimulationCommand request, CancellationToken cancellationToken)
        {
            // first load the Simulation from the repository
            var simulation = await _simulationRepository.GetByIdAsync(request.SimulationId);

            // load the related Match from the repository
            var match = await _matchRepository.GetBySimulationIdAsync(request.SimulationId);

            // load the Clubs from the repository
            var homeClub = await _clubRepository.GetByIdAsync(match.HomeClubId);
            var awayClub = await _clubRepository.GetByIdAsync(match.AwayClubId);

            // link the Clubs with the Simulation, through the Simulation's provided method
            simulation.LinkInvolvingClubsWithSimulation(homeClub, awayClub);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _clubRepository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return  new List<Club>(){ homeClub, awayClub };
        }
    }
}
