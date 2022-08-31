using BettingApp.Services.MatchSimulation.API.Application.Commands;
using BettingApp.Services.MatchSimulation.API.Application.DTOs;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SharedModel;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SimulationAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly ISimulationRepository _simulationRepository;
        private readonly IClubRepository _clubRepository;
        private readonly IMatchRepository _matchRepository;

        public HomeController(ILogger<HomeController> logger,
                              IMediator mediator,
                              ISimulationRepository simulationRepository,
                              IClubRepository clubRepository,
                              IMatchRepository matchRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _simulationRepository = simulationRepository;
            _clubRepository = clubRepository;
            _matchRepository = matchRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome to MatchSimulation!");
        }

        [HttpPost]
        public async Task<IActionResult> CreateSimulationAsync([FromBody] SimulationDTO simulationDto)
        {

            var homeClub = await _clubRepository.GetByIdAsync(simulationDto.HomeClubId);
            var awayClub = await _clubRepository.GetByIdAsync(simulationDto.AwayClubId);
            
            if (homeClub == null || awayClub == null)
            {
                return BadRequest();
            }

            var command = new CreateSimulationCommand(simulationDto.MatchId, homeClub, awayClub,
                                                      simulationDto.LeagueId, simulationDto.KickoffDateTime);

            var result = await _mediator.Send(command);
            
            return Ok(result);

            //throw new NotImplementedException();

        }

        [HttpGet]
        [Route("{simulationId}")]
        public async Task<IActionResult> GetSimulationAsync(string simulationId)
        {
            var simulation = await _simulationRepository.GetByIdAsync(simulationId);

            if (simulation == null) return NotFound();

            return Ok(simulation);
        }

        [HttpDelete]
        [Route("{simulationId}")]
        public async Task<IActionResult> RemoveSimulationAsync(string simulationId)
        {
            throw new NotImplementedException();

            //var removed = _simulationRepository.RemoveById(simulationId);
            //await _simulationRepository.UnitOfWork.SaveChangesAsync();
            //
            //if (removed == false) return NotFound();
            //
            //return Ok(removed);
        }

        [HttpPost]
        public async Task<IActionResult> ProgressAllSimulationsAsync()
        {
            // Retrieve all Simulations from SimulationRepository
            var simulations = await _simulationRepository.GetAllAsync();

            // filer only the Simulations we want, e.g. the Ongoing and the Pending that start now
            simulations = simulations
                            .Where(s => s.StatusId == Status.Ongoing.Id || s.StatusId == Status.Pending.Id)
                            .ToList();

            // send a ProgressSimulationCommand for each Simulation
            foreach (var simulation in simulations)
            {
                try
                {
                    var command = new ProgressSimulationCommand(simulation.Id);
                    var result = await _mediator.Send(command);

                }
                catch (Exception e)
                {
                    _logger.LogInformation($"ERROR sending ProgressSimulationCommand: {e.Message}");
                    continue;
                }    
            }

            return Ok();
        }

        [HttpPost]
        [Route("{simulationId}")]
        public async Task<IActionResult> ProgressSimulationAsync(string simulationId)
        {
            var simulation = await _simulationRepository.GetByIdAsync(simulationId);

            if (simulation == null || simulation.StatusId == Status.Completed.Id || simulation.StatusId == Status.Canceled.Id)
                return BadRequest();

            try
            {
                var command = new ProgressSimulationCommand(simulation.Id);
                var result = await _mediator.Send(command);

            }
            catch (Exception e)
            {
                _logger.LogInformation($"ERROR sending ProgressSimulationCommand: {e.Message}");
            }

            return Ok();
        }

        [HttpPost]
        [Route("{simulationId}")]
        public async Task<IActionResult> ProgressSimulationToCompletionAsync(string simulationId)
        {
            var simulation = await _simulationRepository.GetByIdAsync(simulationId);

            if (simulation == null || simulation.StatusId == Status.Completed.Id || simulation.StatusId == Status.Canceled.Id)
                return BadRequest();

            while (simulation.StatusId != Status.Completed.Id)
            {
                try
                {
                    var command = new ProgressSimulationCommand(simulation.Id);
                    simulation = await _mediator.Send(command);

                }
                catch (Exception e)
                {
                    _logger.LogInformation($"ERROR sending ProgressSimulationCommand: {e.Message}");
                    break;
                }
            }

            return Ok();
        }

        [HttpPost]
        [Route("{simulationId}")]
        public async Task<IActionResult> CancelSimulationAsync(string simulationId)
        {
            var simulation = await _simulationRepository.GetByIdAsync(simulationId);

            if (simulation == null || simulation.StatusId == Status.Completed.Id || simulation.StatusId == Status.Canceled.Id)
                return BadRequest();

            try
            {
                var command = new CancelSimulationCommand(simulation.Id);
                var result = await _mediator.Send(command);

            }
            catch (Exception e)
            {
                _logger.LogInformation($"ERROR sending CancelSimulationCommand: {e.Message}");
            }

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetClubsAsync()
        {
            foreach(var league in League.List())
            {
                if (league.Id == League.NoDomesticLeague.Id || league.Id == League.NoContinentalLeague.Id)
                    continue;

                if(league.TypeId == League.GreekSuperLeague.TypeId)
                {
                    var clubs = await _clubRepository.GetClubsByDomesticLeagueId(league.Id);
                }
                else if(league.TypeId == League.ChampionsLeague.TypeId)
                {
                    var clubs = await _clubRepository.GetClubsByContinentalLeagueId(league.Id);
                }
            }

            return Ok();
        }
    }
}
