using BettingApp.Services.Sportbook.API.DTOs;
using BettingApp.Services.Sportbook.API.Extensions;
using BettingApp.Services.Sportbook.API.IntegrationEvents;
using BettingApp.Services.Sportbook.API.IntegrationEvents.Events;
using BettingApp.Services.Sportbook.API.Model;
using Microsoft.AspNetCore.Mvc;
using MoreLinq.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ISportsbookIntegrationEventService _sportsbookIntegrationEventService;
        private readonly ISportsbookRepository _repository;

        public HomeController(ISportsbookIntegrationEventService sportsbookIntegrationEventService,
                              ISportsbookRepository repository)
        {
            _sportsbookIntegrationEventService = sportsbookIntegrationEventService;
            _repository = repository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome to Sportsbook!");
        }

        [HttpPost]
        public async Task<IActionResult> RequestToAddSelectionAsync([FromQuery]string matchId, [FromQuery]int gamblerMatchResultId,
                                                                    [FromHeader(Name = "x-requestid")] string requestId)
        {
            // Normally at this point, we get the GamblerId by the IdentityService
            //var gamblerId = "ac97804c-f777-4dd5-b33e-735b42f81dcf";
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            var match = await _repository.GetMatchAsync(matchId);

            if (match == null)
            {
                return NotFound();
            }

            // if match or possiblePick is not betable, deny the request
            var possiblePick = match.PossiblePicks.SingleOrDefault(p => p.MatchResultId == gamblerMatchResultId);
            
            if (!match.IsBetable || !possiblePick.IsBetable)
            {
                return BadRequest();
            }

            var selectionDto = match.ToSelectionDTO(gamblerMatchResultId);

            var integrationEvent = new UserRequestedToAddSelectionIntegrationEvent(gamblerId, selectionDto, Guid.Parse(requestId).ToString());

            await _sportsbookIntegrationEventService.SaveEventAndSportsbookContextChangesAsync(integrationEvent);
            await _sportsbookIntegrationEventService.PublishThroughEventBusAsync(integrationEvent);

            return Ok(requestId);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllMatchesAsync()
        {
            var matches = await _repository.GetAllMatchesAsync();

            return Ok(matches);
        }

        [HttpGet]
        public async Task<IActionResult> GetMatchesByDateAsync([FromQuery]DateTime date)
        {
            var matches = await _repository.GetMatchesByDate(date);
            var matchDTOs = matches?.Select(m => m.ToMatchDTO());
            return Ok(matchDTOs);
        }

        [HttpGet]
        public async Task<IActionResult> GetLeaguesByDateAsync([FromQuery] DateTime date)
        {
            var matches = await _repository.GetMatchesByDate(date);
            var distinctByLeagueMatches = matches.DistinctBy(m => m.LeagueId);
            var leagueDTOs = distinctByLeagueMatches?.Select(m => League.From(m.LeagueId))
                                                    .Select(l => l.ToLeagueDTO())
                                                    .OrderBy(l => l.Id);
            
            return Ok(leagueDTOs);
        }

        [HttpGet]
        public IActionResult GetMatchResults()
        {
            var matchResultDTOs = MatchResult.List()
                                             ?.Select(mr => mr.ToMatchResultDTO())
                                             .OrderBy(mr => mr.Id);
            
            return Ok(matchResultDTOs);
        }

        [HttpGet]
        public async Task<IActionResult> GetMatchAsync([FromQuery] string matchId)
        {
            var match = await _repository.GetMatchAsync(matchId);

            if (match == null) return NotFound();

            return Ok(match.ToMatchDTO());
        }

        [HttpGet]
        public async Task<IActionResult> GetSelectionAsync([FromQuery] string matchId, [FromQuery]int matchResultId)
        {
            var match = await _repository.GetMatchAsync(matchId);

            if (match == null) return NotFound();

            return Ok(match.ToSelectionDTO(matchResultId));
        }

    }
}
