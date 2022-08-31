using BettingApp.Services.MatchSimulation.API.Application.Commands;
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
    public class CancelUnstartedSimulationsJob : IJob
    {
        private readonly ILogger<CancelUnstartedSimulationsJob> _logger;
        private readonly IMediator _mediator;
        private readonly ISimulationRepository _simulationRepository;
        private readonly IMatchRepository _matchRepository;

        public CancelUnstartedSimulationsJob(ILogger<CancelUnstartedSimulationsJob> logger,
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
            _logger.LogInformation(".:: Starting execution of CancelUnstartedSimulationsJob ::.");

            // first load all the Pending Simulations from the repository
            var pendingSimulations = await _simulationRepository.GetByStatusIdAsync(Status.Pending.Id);

            // filter only the Pending Simulations with KickoffDateTime late more than 5 minutes
            // from the current DateTime, and add them to a separate list containing Simulations to be canceled
            var simulationsToCancel = new List<Simulation>();
            foreach(var simulation in pendingSimulations)
            {
                // load the related match from the repository
                var match = await _matchRepository.GetByIdAsync(simulation.MatchId);

                // calculate the TimeSpan between current DateTime and kickoff DateTime
                double minutesPassedSinceKickoff = (DateTime.UtcNow.AddHours(2) - match.KickoffDateTime).TotalMinutes;

                // add the Simulation to the filtered list, if timespan is greater than 5 minutes
                if (minutesPassedSinceKickoff > 5)
                    simulationsToCancel.Add(simulation);
            }

            // send a CancelSimulationCommand for each Simulation in the filtered list
            foreach(var simulation in simulationsToCancel)
            {
                try
                {
                    await _mediator.Send(new CancelSimulationCommand(simulation.Id));
                }
                catch (Exception e)
                {
                    // if an exception is thrown, log it and continue with next simulation
                    _logger.LogInformation($"ERROR while sending ProgressSimulationCommand " +
                                            $"for Simulation {simulation.Id}: \"{e.Message}\"");
                }
            }

            _logger.LogInformation(".:: Ending execution of CancelUnstartedSimulationsJob ::.");
        }
    }
}
