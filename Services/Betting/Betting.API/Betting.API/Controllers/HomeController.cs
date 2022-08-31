using BettingApp.Services.Betting.API.Application.Commands;
using BettingApp.Services.Betting.API.Application.DTOs;
using BettingApp.Services.Betting.API.Extensions;
using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using BettingApp.Services.Betting.Infrastructure;
using BettingApp.Services.Betting.Infrastructure.Idempotency;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace BettingApp.Services.Betting.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<HomeController> _logger;
        private readonly IBetRepository _repository;
        private readonly BettingContext _context;
        private readonly IRequestManager _requestManager;

        public HomeController(IMediator mediator, 
                                ILogger<HomeController> logger, 
                                IBetRepository repository, 
                                BettingContext context,
                                IRequestManager requestManager)
        {
            _mediator = mediator;
            _logger = logger;
            _repository = repository;
            _context = context;
            _requestManager = requestManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome!");
        }

        [HttpGet]
        public async Task<IActionResult> GetBetAsync([FromQuery]string betId)
        {
            // normally at this point we get gamblerId from IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            var bet = await _repository.GetByBetIdAndGamblerId(gamblerId, betId);

            if (bet == null)
            {
                return NotFound();
            }

            return Ok(bet.ToBetDTO());
        }

        [HttpGet]
        public async Task<IActionResult> GetBetPreviewsPageAsync(int pageNumber)
        {
            // get gamblerId from IdentityService
            // something like: var gamblerId = _identityService.User.Claims["sub"];
            //var gamblerId = "ac97804c-f777-4dd5-b33e-735b42f81dcf";
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            var betsPage = await _repository.GetBetsPageByGamblerId(gamblerId, pageNumber);
            var betPreviewDTOs = betsPage?.Select(b => b.ToBetPreviewDTO());

            return Ok(betPreviewDTOs);
        }

        [HttpGet]
        public IActionResult GetBetPreviewsPagesCount()
        {
            // Normally at this point we get gamblerId from IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";
            var totalPages = _repository.GetBetPreviewsPagesCount(gamblerId);
            return Ok(totalPages);
        }

        [HttpPost]
        public async Task<IActionResult> CancelBetAsync([FromQuery]string betId)
        {
            // normally at this point we get the gamblerId from IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            var bet = await _repository.GetByBetIdAndGamblerId(gamblerId, betId);

            if (bet == null)
            {
                return NotFound();
            }

            if (!bet.IsCancelable)
            {
                return BadRequest();
            }

            var command = new CancelBetCommand(betId);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> RequestExists([FromQuery] string requestId)
        {
            // normally at this point we get the gamblerId from IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            try
            {
                //var found = await _requestManager.ExistWithUserIdAsync(Guid.Parse(requestId), userId: gamblerId);
                var found = await _requestManager.ExistAsync(Guid.Parse(requestId));

                if (found)
                    return Ok(true);

                return NotFound();
            }
            catch (Exception e)
            {
                return BadRequest();
            }
        }

        // ==========================

        //[HttpPost]
        //public async Task<IActionResult> CreateBetAsync([FromBody] BetslipDTO betslipDto, [FromHeader(Name = "x-requestid")] string requestId)
        //{
        //    _logger.LogInformation($"A CreateBetCommand will be sent.");
        //
        //    var embeddedCommand = new CreateBetCommand(Guid.NewGuid().ToString(), betslipDto.GamblerId, betslipDto.WageredAmount, betslipDto.SelectionDTOs);
        //    var command = new IdentifiedCommand<CreateBetCommand, Bet>(embeddedCommand, Guid.Parse(requestId));
        //
        //    var result = await _mediator.Send(command);
        //
        //    _logger.LogInformation($"Result: {result}");
        //
        //    return Ok(result);
        //
        //}

        [HttpPost]
        [Route("{matchId}")]
        public async Task<IActionResult> UpdateMatchCurrentMinuteAsync(string matchId, [FromQuery] string newMinute)
        {
            _logger.LogInformation($"An UpdateMatchCurrentMinuteCommand will be sent.");

            var command = new UpdateMatchCurrentMinuteCommand(matchId, newMinute);
            
            var result = await _mediator.Send(command);

            _logger.LogInformation($"Result: {result}");

            return Ok(result);

        }

        [HttpPost]
        [Route("{matchId}")]
        public async Task<IActionResult> UpdateMatchScoresAsync(string matchId, 
                                         [FromQuery] int homeScore, [FromQuery] int awayScore)
        {
            _logger.LogInformation($"An UpdateMatchScoresCommand will be sent.");

            var command = new UpdateMatchScoresCommand(matchId, homeScore, awayScore);

            var result = await _mediator.Send(command);

            _logger.LogInformation($"Result: {result}");

            return Ok(result);

        }

        //[HttpPost]
        //[Route("{betId}")]
        //public async Task<IActionResult> MarkBetAsPaid(string betId, [FromHeader(Name = "x-requestid")] string requestId)
        //{
        //    _logger.LogInformation($"A MarkBetAsPaidCommand will be sent.");
        //
        //    var embeddedCommand = new MarkBetAsPaidCommand(betId);
        //
        //    var command = new IdentifiedCommand<MarkBetAsPaidCommand,bool>(embeddedCommand, Guid.Parse(requestId));
        //
        //    var result = await _mediator.Send(command);
        //
        //    _logger.LogInformation($"Result: {result}");
        //
        //    return Ok(result);
        //
        //}

    }
}

