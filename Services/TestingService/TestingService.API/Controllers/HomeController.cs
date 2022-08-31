using BettingApp.Services.TestingService.API.IntegrationEvents;
using BettingApp.Services.TestingService.API.IntegrationEvents.Events;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.TestingService.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class HomeController : Controller
    {
        private readonly ITestingIntegrationEventService _testingIntegrationEventService;

        public HomeController(ITestingIntegrationEventService testingIntegrationEventService)
        {
            _testingIntegrationEventService = testingIntegrationEventService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return Ok("Welcome to TestingService!");
        }

        [HttpPost]
        public async Task<IActionResult> TestPublishMatchCreatedIntegrationEventAsync()
        {
            // publish an integration event here
            var possiblePickDTOs = new List<PossiblePickDTO>();

            possiblePickDTOs.Add(new PossiblePickDTO(1, (decimal)1.50, 1, 0));
            possiblePickDTOs.Add(new PossiblePickDTO(2, (decimal)2.10, 1, 0));
            possiblePickDTOs.Add(new PossiblePickDTO(3, (decimal)3.00, 1, 0));
            possiblePickDTOs.Add(new PossiblePickDTO(4, (decimal)1.90, 1, 0));
            possiblePickDTOs.Add(new PossiblePickDTO(5, (decimal)1.65, 1, 0));

            var matchDto = new MatchDTO("1478", "Panetolikos", "Larisa", DateTime.UtcNow.AddHours(2), "0", 0, 0, possiblePickDTOs);

            var matchCreatedIntegrationEvent = new MatchCreatedIntegrationEvent(matchDto);

            await _testingIntegrationEventService.SaveEventAndTestingContextChangesAsync(matchCreatedIntegrationEvent);
            await _testingIntegrationEventService.PublishThroughEventBusAsync(matchCreatedIntegrationEvent);

            return Accepted();
        }

        [HttpPost]
        [Route("{matchId}")]
        public async Task<IActionResult> TestPublishMatchCurrentMinuteChangedIntegrationEventAsync(string matchId, [FromQuery]string newCurrentMinute)
        {
            var matchCurrentMinuteChangedIntegrationEvent = new MatchCurrentMinuteChangedIntegrationEvent(matchId, newCurrentMinute);

            await _testingIntegrationEventService.SaveEventAndTestingContextChangesAsync(matchCurrentMinuteChangedIntegrationEvent);
            await _testingIntegrationEventService.PublishThroughEventBusAsync(matchCurrentMinuteChangedIntegrationEvent);

            return Accepted();
        }

        [HttpPost]
        [Route("{matchId}")]
        public async Task<IActionResult> TestPublishMatchScoresChangedIntegrationEventAsync(string matchId, 
                                                                                            [FromQuery]int newHomeClubScore,
                                                                                            [FromQuery]int newAwayClubScore)
        {
            var matchScoresChangesIntegrationEvent = new MatchScoresChangedIntegrationEvent(matchId,newHomeClubScore,newAwayClubScore);

            await _testingIntegrationEventService.SaveEventAndTestingContextChangesAsync(matchScoresChangesIntegrationEvent);
            await _testingIntegrationEventService.PublishThroughEventBusAsync(matchScoresChangesIntegrationEvent);

            return Accepted();
        }


        [HttpPost]
        [Route("{matchId}")]
        public async Task<IActionResult> TestPublishMatchResultOddChangedIntegrationEventAsync(string matchId, [FromQuery]int resultId, [FromQuery]decimal newOdd)
        {
            var matchResultOddChangedIntegrationEvent = new MatchResultOddChangedIntegrationEvent(matchId, resultId, newOdd);

            await _testingIntegrationEventService.SaveEventAndTestingContextChangesAsync(matchResultOddChangedIntegrationEvent);
            await _testingIntegrationEventService.PublishThroughEventBusAsync(matchResultOddChangedIntegrationEvent);

            return Accepted();
        }

        [HttpPost]
        [Route("{matchId}")]
        public async Task<IActionResult> TestPublishMatchResultEnabledIntegrationEventAsync(string matchId, [FromQuery] int resultId)
        {
            var matchResultEnabledIntegrationEvent = new MatchResultEnabledIntegrationEvent(matchId, resultId);

            await _testingIntegrationEventService.SaveEventAndTestingContextChangesAsync(matchResultEnabledIntegrationEvent);
            await _testingIntegrationEventService.PublishThroughEventBusAsync(matchResultEnabledIntegrationEvent);

            return Accepted();
        }

        [HttpPost]
        [Route("{matchId}")]
        public async Task<IActionResult> TestPublishMatchResultDisabledIntegrationEventAsync(string matchId, [FromQuery] int resultId)
        {
            var matchResultDisabledIntegrationEvent = new MatchResultDisabledIntegrationEvent(matchId, resultId);

            await _testingIntegrationEventService.SaveEventAndTestingContextChangesAsync(matchResultDisabledIntegrationEvent);
            await _testingIntegrationEventService.PublishThroughEventBusAsync(matchResultDisabledIntegrationEvent);

            return Accepted();
        }

        [HttpPost]
        [Route("{matchId}")]
        public async Task<IActionResult> TestPublishMatchCanceledIntegrationEventAsync(string matchId)
        {
            var matchCanceledIntegrationEvent = new MatchCanceledIntegrationEvent(matchId);

            await _testingIntegrationEventService.SaveEventAndTestingContextChangesAsync(matchCanceledIntegrationEvent);
            await _testingIntegrationEventService.PublishThroughEventBusAsync(matchCanceledIntegrationEvent);

            return Accepted();
        }

        [HttpPost]
        [Route("{gamblerId}")]
        public async Task<IActionResult> TestPublishBetMarkedAsPaidIntegrationEventAsync(string gamblerId)
        {
            var betMarkedAsPaidIntegrationEvent = new BetMarkedAsPaidIntegrationEvent(gamblerId);

            await _testingIntegrationEventService.SaveEventAndTestingContextChangesAsync(betMarkedAsPaidIntegrationEvent);
            await _testingIntegrationEventService.PublishThroughEventBusAsync(betMarkedAsPaidIntegrationEvent);

            return Accepted();
        }

        [HttpPost]
        [Route("{gamblerId}")]
        public async Task<IActionResult> TestPublishUserWalletBalanceChangedIntegrationEventAsync(string gamblerId, [FromQuery]decimal newBalance)
        {
            var userWalletBalanceChangedIntegrationEvent = new UserWalletBalanceChangedIntegrationEvent(gamblerId,newBalance);

            await _testingIntegrationEventService.SaveEventAndTestingContextChangesAsync(userWalletBalanceChangedIntegrationEvent);
            await _testingIntegrationEventService.PublishThroughEventBusAsync(userWalletBalanceChangedIntegrationEvent);

            return Accepted();
        }


        [HttpPost]
        [Route("{gamblerId}")]
        public async Task<IActionResult> TestPublishBetPaymentSucceededIntegrationEventAsync(string gamblerId, [FromQuery]string betId)
        {
            var betPaymentSucceededIntegrationEvent = new BetPaymentSucceededIntegrationEvent(gamblerId, betId);

            await _testingIntegrationEventService.SaveEventAndTestingContextChangesAsync(betPaymentSucceededIntegrationEvent);
            await _testingIntegrationEventService.PublishThroughEventBusAsync(betPaymentSucceededIntegrationEvent);

            return Accepted();
        }

        [HttpPost]
        [Route("{gamblerId}")]
        public async Task<IActionResult> TestPublishBetPaymentFailedIntegrationEventAsync(string gamblerId, [FromQuery] string betId)
        {
            var betPaymentFailedIntegrationEvent = new BetPaymentFailedIntegrationEvent(gamblerId, betId);

            await _testingIntegrationEventService.SaveEventAndTestingContextChangesAsync(betPaymentFailedIntegrationEvent);
            await _testingIntegrationEventService.PublishThroughEventBusAsync(betPaymentFailedIntegrationEvent);

            return Accepted();
        }

        [HttpPost]
        [Route("{gamblerId}")]
        public async Task<IActionResult> TestPublishBetCanceledIntegrationEventAsync(string gamblerId,
                                                                                     [FromQuery] string betId,
                                                                                     [FromQuery] decimal wageredAmount)
        {
            var betCanceledIntegrationEvent = new BetCanceledIntegrationEvent(betId, gamblerId, wageredAmount);

            await _testingIntegrationEventService.SaveEventAndTestingContextChangesAsync(betCanceledIntegrationEvent);
            await _testingIntegrationEventService.PublishThroughEventBusAsync(betCanceledIntegrationEvent);

            return Accepted();
        }
    }
}
