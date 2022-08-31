using BettingApp.Services.Betslips.API.Application.Commands;
using BettingApp.Services.Betslips.API.Application.DTOs;
using BettingApp.Services.Betslips.API.Application.IntegrationEvents;
using BettingApp.Services.Betslips.API.Application.IntegrationEvents.Events;
using BettingApp.Services.Betslips.API.Extensions;
using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using BettingApp.Services.Betslips.Domain.AggregatesModel.WalletAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly IBetslipRepository _betslipRepository;
        private readonly IWalletRepository _walletRepository;
        private readonly IBetslipsIntegrationEventService _betslipsIntegrationEventService;

        public HomeController(ILogger<HomeController> logger,
                              IMediator mediator,
                              IBetslipRepository betslipRepository,
                              IWalletRepository wallerRepository,
                              IBetslipsIntegrationEventService betslipsIntegrationEventService)
        {
            _logger = logger;
            _mediator = mediator;
            _betslipRepository = betslipRepository;
            _walletRepository = wallerRepository;
            _betslipsIntegrationEventService = betslipsIntegrationEventService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome to Betslips!");
        }

        [HttpGet]
        public async Task<IActionResult> GetBetslipAsync()
        {
            // Normally at this point, we get the GamblerId by the IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            var betslip = await _betslipRepository.GetByGamblerIdAsync(gamblerId);

            if(betslip == null)
            {
                _logger.LogInformation($"A CreateBetslipCommand will be sent.");
                var command = new CreateBetslipCommand(gamblerId);

                betslip = await _mediator.Send(command);

            }

            return Ok(betslip.ToBetslipDTO());
        }

        [HttpGet]
        public async Task<IActionResult> CheckAddSelectionAsync([FromQuery]string matchId, [FromQuery] int matchResultId)
        {
            // Normally at this point, we get the GamblerId by the IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            var exists = _betslipRepository.ExistsWithGamblerId(gamblerId);

            if (!exists) return Ok(true);

            var betslip = await _betslipRepository.GetByGamblerIdAsync(gamblerId);

            if (betslip.CanAddSelection(matchId, matchResultId))
            {
                return Ok(true);
            }

            return Ok(false);
        } 

        [HttpPost]
        public async Task<IActionResult> AddSelectionAsync([FromBody] SelectionDTO selectionDto)
        {
            // Normally at this point, we get the GamblerId by the IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            var exists = _betslipRepository.ExistsWithGamblerId(gamblerId);

            if(!exists)
            {
                await _mediator.Send(new CreateBetslipCommand(gamblerId));
            }

            if (!selectionDto.IsBetable)
            {
                return BadRequest("Selection cannot be added to betslip.");
            }

            var betslip = await _betslipRepository.GetByGamblerIdAsync(gamblerId);
            if(!betslip.CanAddSelection(selectionDto.RelatedMatchId, selectionDto.GamblerMatchResultId))
            {
                return BadRequest("Selection cannot be added to betslip.");
            }

            var command = new AddSelectionCommand(gamblerId, selectionDto.GamblerMatchResultId, 
                                                selectionDto.Odd, selectionDto.InitialOdd, selectionDto.RelatedMatchId, 
                                                selectionDto.HomeClubName, selectionDto.AwayClubName, 
                                                selectionDto.KickoffDateTime, selectionDto.CurrentMinute,
                                                selectionDto.HomeClubScore, selectionDto.AwayClubScore, 
                                                selectionDto.RequirementTypeId, selectionDto.RequiredValue, String.Empty);

            var result = await _mediator.Send(command);

            return Ok(result.ToBetslipDTO());
        }

        [HttpPost]
        public async Task<IActionResult> RemoveSelectionAsync([FromQuery] string selectionId)
        {
            // Normally at this point, we get the GamblerId by the IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            var exists = _betslipRepository.ExistsWithGamblerId(gamblerId);

            if (!exists)
            {
                var newBetslip = await _mediator.Send(new CreateBetslipCommand(gamblerId));
                return Ok(newBetslip);
            }

            var betslip = await _betslipRepository.GetByGamblerIdAsync(gamblerId);
            if (!betslip.Selections.Any(s => s.Id.Equals(selectionId)))
            {
                return NotFound();
            }

            var command = new RemoveSelectionCommand(gamblerId, selectionId);
            betslip = await _mediator.Send(command);

            return Ok(betslip);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateWageredAmountAsync([FromQuery] decimal wageredAmount)
        {
            // Normally at this point, we get the GamblerId by the IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            var exists = _betslipRepository.ExistsWithGamblerId(gamblerId);

            if (!exists)
            {
                await _mediator.Send(new CreateBetslipCommand(gamblerId));
            }

            var command = new UpdateWageredAmountCommand(gamblerId, wageredAmount);
            var result = await _mediator.Send(command);

            return Ok(result);
        }


        [HttpDelete]
        public async Task<IActionResult> ClearBetslipAsync()
        {
            // Normally at this point, we get the GamblerId by the IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            var exists = _betslipRepository.ExistsWithGamblerId(gamblerId);

            if (!exists) 
                return Ok(true);

            var command = new ClearBetslipCommand(gamblerId);
            var result = await _mediator.Send(command);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CheckoutAsync([FromHeader(Name = "x-requestid")] string requestId)
        {
            // Normally at this point, we get the GamblerId by the IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            var betslip = await _betslipRepository.GetByGamblerIdAsync(gamblerId);
            if (betslip == null || !betslip.IsBetable)
            {
                return BadRequest("Betslip is not betable.");
            }

            // Check if wallet's balance is enough for the wagered amount, before publishing the IntegrationEvent
            var wallet = await _walletRepository.GetByGamblerIdAsync(gamblerId);
            if (wallet == null || wallet.Balance < betslip.WageredAmount)
            {
                return BadRequest("Wallet balance not enough for wagered amount.");
            }

            // Once betslip is checkout, sends an integration event to
            // betting.api to convert betslip to bet and proceed with
            // bet creation process
            var integrationEvent = new UserCheckedOutIntegrationEvent(Guid.Parse(requestId), betslip.ToBetslipDTO());

            await _betslipsIntegrationEventService.SaveEventAndBetslipsContextChangesAsync(integrationEvent);
            await _betslipsIntegrationEventService.PublishThroughEventBusAsync(integrationEvent);

            return Ok(requestId);
        }

        [HttpGet]
        public async Task<IActionResult> GetWalletBalance()
        {
            // Normally at this point, we get the GamblerId by the IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            var wallet = await _walletRepository.GetByGamblerIdAsync(gamblerId);

            if (wallet == null)
            {
                wallet = await _mediator.Send(new CreateWalletCommand(gamblerId));
            }

            return Ok(wallet.Balance.ToString());
        }

        [HttpGet]
        public async Task<IActionResult> VerifyLatestAddition([FromQuery] string latestAdditionId)
        {
            // Normally at this point, we get the GamblerId by the IdentityService
            var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";

            var betslip = await _betslipRepository.GetByGamblerIdAsync(gamblerId);
            if (betslip == null)
                return NotFound();

            if (! betslip.LatestAdditionIdMatches(latestAdditionId))
                return NotFound();

            return Ok();
        }

        // ==================

        [HttpPost]
        [Route("{gamblerId}")]
        public async Task<IActionResult> AddSelectionIdentifiedAsync(string gamblerId, [FromBody] SelectionDTO selectionDto,
                                                                     [FromHeader(Name = "x-requestid")] Guid requestId)
        {
            var exists = _betslipRepository.ExistsWithGamblerId(gamblerId);

            if (!exists)
            {
                _logger.LogInformation($"A CreateBetslipCommand will be sent.");
                await _mediator.Send(new CreateBetslipCommand(gamblerId));
            }

            _logger.LogInformation($"An AddSelectionCommand will be sent.");

            var command = new AddSelectionCommand(gamblerId, selectionDto.GamblerMatchResultId,
                                                selectionDto.Odd, selectionDto.InitialOdd, selectionDto.RelatedMatchId,
                                                selectionDto.HomeClubName, selectionDto.AwayClubName,
                                                selectionDto.KickoffDateTime, selectionDto.CurrentMinute,
                                                selectionDto.HomeClubScore, selectionDto.AwayClubScore,
                                                selectionDto.RequirementTypeId, selectionDto.RequiredValue, String.Empty);

            var identifiedCommand = new IdentifiedCommand<AddSelectionCommand,Betslip>(command, requestId);

            var result = await _mediator.Send(identifiedCommand);

            return Ok(result);
        }

        [HttpPut]
        [Route("{matchId}")]
        public async Task<IActionResult> UpdateMatchCurrentMinuteAsync(
                                                    string matchId, [FromQuery] string newMinute)
        {
            _logger.LogInformation($"An UpdateMatchCurrentMinuteCommand will be sent.");

            var command = new UpdateMatchCurrentMinuteCommand(matchId, newMinute);

            var result = await _mediator.Send(command);

            return Ok(result);
        }


        [HttpPut]
        [Route("{matchId}")]
        public async Task<IActionResult> CancelMatchAsync(string matchId)
        {
            _logger.LogInformation($"An CancelMatchCommand will be sent.");

            var command = new CancelMatchCommand(matchId);

            var result = await _mediator.Send(command);

            return Ok(result);
        }


        [HttpPut]
        [Route("{selectionId}")]
        public async Task<IActionResult> UpdateSelectionOddAsync(string selectionId, [FromQuery]decimal newOdd)
        {
            _logger.LogInformation($"An UpdateSelectionOddCommand will be sent.");

            var command = new UpdateSelectionOddCommand(selectionId, newOdd);

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
