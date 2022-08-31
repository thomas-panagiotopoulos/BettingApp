using BettingApp.Services.Wallets.API.DTOs;
using BettingApp.Services.Wallets.API.Extensions;
using BettingApp.Services.Wallets.API.Infrastructure.Services;
using BettingApp.Services.Wallets.API.IntegrationEvents;
using BettingApp.Services.Wallets.API.IntegrationEvents.Events;
using BettingApp.Services.Wallets.API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.Controllers
{
    [Authorize(Policy = "WalletsApiScope")]
    [ApiController]
    [Route("[controller]/[action]")]
    public class WalletsController : Controller
    {
        private readonly IWalletsRepository _repository;
        private readonly IWalletsIntegrationEventService _walletsIntegrationEventService;
        private readonly IIdentityService _identityService;

        public WalletsController(IWalletsRepository repository,
                              IWalletsIntegrationEventService walletsIntegrationEventService,
                              IIdentityService identityService)
        {
            _repository = repository;
            _walletsIntegrationEventService = walletsIntegrationEventService;
            _identityService = identityService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome to Wallets!");
        }

        [HttpGet]
        public async Task<IActionResult> GetWalletPreviewAsync()
        {
            // Normally at this point, we get the GamblerId by the IdentityService
            //var gamblerId = "ac97804c-f777-4dd5-b33e-735b42f81dcf";
            //var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";
            var gamblerId = _identityService.GetUserIdentity();

            if (!_repository.WalletExistsWithGamblerId(gamblerId))
            {
                _repository.AddWallet(new Wallet(gamblerId));
                await _repository.UnitOfWork.SaveChangesAsync();
            }

            var wallet = _repository.GetWalletByGamblerIdWithoutTransactions(gamblerId);
            var walletPreviewDto = wallet.ToWalletPreviewDTO();

            return Ok(walletPreviewDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetTransactionsPageAsync(int pageNumber)
        {
            // Normally at this point, we get the GamblerId by the IdentityService
            //var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";
            var gamblerId = _identityService.GetUserIdentity();

            if (!_repository.WalletExistsWithGamblerId(gamblerId))
            {
                _repository.AddWallet(new Wallet(gamblerId));
                await _repository.UnitOfWork.SaveChangesAsync();
            }

            var transactionsPage = _repository.GetTransactionsPage(gamblerId, pageNumber);
            var transactionDTOs = transactionsPage?.Select(t => t.ToTransactionDTO()).ToList();

            return Ok(transactionDTOs);
        }

        [HttpGet]
        public IActionResult GetTransactionsPagesCount()
        {
            // Normally at this point, we get the GamblerId by the IdentityService
            //var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";
            var gamblerId = _identityService.GetUserIdentity();

            var transactionsPagesCount = _repository.GetTransactionsPagesCount(gamblerId);
            return Ok(transactionsPagesCount);
        }

        [HttpPost]
        public async Task<IActionResult> RequestTopUpAsync([FromBody] RequestTopUpDTO requestTopUpDto,
                                                           [FromHeader(Name = "x-requestid")] string requestId)
        {
            if (requestTopUpDto.Amount <= 0)
            {
                return BadRequest("TopUp amount too low");
            }

            // Normally at this point, we get the GamblerId by the IdentityService
            //var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";
            var gamblerId = _identityService.GetUserIdentity();

            var wallet = _repository.GetWalletByGamblerId(gamblerId);

            if (wallet == null)
            {
                return BadRequest("Wallet not found");
            }

            if (wallet != null)
            {
                var transactionExists = wallet.TransactionExists(TransactionType.TopUp.Id, requestId);
                if (transactionExists)
                    return Ok(requestId);
            }

            var requestTopUpIntegrationEvent = new UserRequestedTopUpIntegrationEvent(gamblerId, requestTopUpDto.Amount,
                                                                                    requestTopUpDto.CardNumber, requestTopUpDto.SecurityNumber,
                                                                                    requestTopUpDto.CardHolderName,
                                                                                    requestTopUpDto.ExpirationDateMM, requestTopUpDto.ExpirationDateYY,
                                                                                    requestTopUpDto.CardTypeId, requestTopUpDto.CardTypeName,
                                                                                    requestId);

            await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(requestTopUpIntegrationEvent);
            await _walletsIntegrationEventService.PublishThroughEventBusAsync(requestTopUpIntegrationEvent);

            return Ok(requestId);
        }

        [HttpPost]
        public async Task<IActionResult> RequestWithdrawAsync([FromBody] RequestWithdrawDTO requestWithdrawDTO,
                                                           [FromHeader(Name = "x-requestid")] string requestId)
        {
            if (requestWithdrawDTO.Amount <= 0)
            {
                return BadRequest("Withdraw amount too low");
            }

            // Normally at this point, we get the GamblerId by the IdentityService
            //var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";
            var gamblerId = _identityService.GetUserIdentity();

            var wallet = _repository.GetWalletByGamblerId(gamblerId);

            if (wallet == null)
            {
                return BadRequest("Wallet not found");
            }
            else
            {
                if (wallet.TransactionExists(TransactionType.Withdraw.Id, requestId))
                {
                    return Ok(requestId);
                }

                if (wallet.Balance < requestWithdrawDTO.Amount)
                {
                    return BadRequest("Not sufficient Wallet balance.");
                }

                if (!wallet.WithdrawPreservesWelcomeBonus(requestWithdrawDTO.Amount))
                {
                    return BadRequest("Cannot Withdraw any unused WelcomeBonus amount.");
                }
            }

            var requestWithdrawIntegrationEvent = new UserRequestedWithdrawIntegrationEvent(gamblerId, requestWithdrawDTO.Amount,
                                                                                            requestWithdrawDTO.IBAN, requestId);

            await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(requestWithdrawIntegrationEvent);
            await _walletsIntegrationEventService.PublishThroughEventBusAsync(requestWithdrawIntegrationEvent);

            return Ok(requestId);

        }

        [HttpGet]
        public IActionResult TopUpRequestExists([FromQuery] string requestId)
        {
            // Normally at this point, we get the GamblerId by the IdentityService
            //var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";
            var gamblerId = _identityService.GetUserIdentity();

            var wallet = _repository.GetWalletByGamblerId(gamblerId);

            if (wallet == null)
            {
                return BadRequest("Wallet not found");
            }
            else
            {
                if (wallet.TransactionExists(TransactionType.TopUp.Id, requestId))
                    return Ok(true);

                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult WithdrawRequestExists([FromQuery] string requestId)
        {
            // Normally at this point, we get the GamblerId by the IdentityService
            //var gamblerId = "252a56ee-5b57-4026-b934-1633661226bd";
            var gamblerId = _identityService.GetUserIdentity();

            var wallet = _repository.GetWalletByGamblerId(gamblerId);

            if (wallet == null)
            {
                return BadRequest("Wallet not found");
            }
            else
            {
                if (wallet.TransactionExists(TransactionType.Withdraw.Id, requestId))
                    return Ok(true);

                return NotFound();
            }
        }
    }
}
