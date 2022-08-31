using BettingApp.Services.MatchSimulation.API.Application.Commands;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SimulationAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.BackgroundTasks.Jobs
{
    [DisallowConcurrentExecution]
    public class ProgressSimulationsJob : IJob
    {
        private readonly ILogger<ProgressSimulationsJob> _logger;
        private readonly IMediator _mediator;
        private readonly ISimulationRepository _simulationRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IClubRepository _clubRepository;

        public ProgressSimulationsJob(ILogger<ProgressSimulationsJob> logger,
                                           IMediator mediator,
                                           ISimulationRepository simulationRepository,
                                           IMatchRepository matchRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _simulationRepository = simulationRepository;
            _matchRepository = matchRepository;

        }

        public async Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation(".:: Starting execution of ProgressSimulationsJob ::.");

            // first load all the Ongoing Simulations from the repository
            var ongoingSimulations = await _simulationRepository.GetByStatusIdAsync(Status.Ongoing.Id);

            // then load all the Pending Simulations from the repository
            var pendingSimulations = await _simulationRepository.GetByStatusIdAsync(Status.Pending.Id);

            // filter only the Pending Simulations with KickoffDateTime equal to current DateTime
            // or 5 minutes past at maximum
            var filteredPendingSimulations = new List<Simulation>();
            foreach(var simulation in pendingSimulations)
            {
                // load the related match from the repository
                var match = await _matchRepository.GetByIdAsync(simulation.MatchId);

                // calculate the TimeSpan between current DateTime and kickoff DateTime
                double minutesPassedSinceKickoff = (DateTime.UtcNow.AddHours(2) - match.KickoffDateTime).TotalMinutes;
                
                // add the Simulation to the filtered list, if timespan is between 5 minutes and 0 minutes
                if (minutesPassedSinceKickoff >= 0 && minutesPassedSinceKickoff <= 5)
                    filteredPendingSimulations.Add(simulation);
            }

            // merge Ongoing Simulations and filtered Pending Simulations in a single List
            var simulationsToProgress = new List<Simulation>();
            ongoingSimulations.ForEach(s => simulationsToProgress.Add(s));
            filteredPendingSimulations.ForEach(s => simulationsToProgress.Add(s));

            // send a ProgressSimulationCommand for each Simulation
            foreach(var simulation in simulationsToProgress)
            {
                try
                {
                    await _mediator.Send(new ProgressSimulationCommand(simulation.Id));
                }
                catch(Exception e)
                {
                    // if an exception is thrown, log it and continue with next simulation
                    _logger.LogInformation($"ERROR while sending ProgressSimulationCommand " +
                                            $"for Simulation {simulation.Id}: \"{e.Message}\"");
                }
            }

            _logger.LogInformation(".:: Ending execution of ProgressSimulationsJob ::.");
        }
    }
}
