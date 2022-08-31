using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Betting.SignalrHub.IntegrationEvents.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.SignalrHub.IntegrationEvents.EventHandling
{
    public class BetSelectionMatchScoresChangedIntegrationEventHandler
        : IIntegrationEventHandler<BetSelectionMatchScoresChangedIntegrationEvent>
    {
        private readonly ILogger<BetSelectionMatchScoresChangedIntegrationEventHandler> _logger;
        private readonly IHubContext<BettingHub> _hubContext;

        public BetSelectionMatchScoresChangedIntegrationEventHandler(
                                                    ILogger<BetSelectionMatchScoresChangedIntegrationEventHandler> logger,
                                                    IHubContext<BettingHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task Handle(BetSelectionMatchScoresChangedIntegrationEvent @event)
        {
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            await _hubContext.Clients
                            .Group(@event.GamblerId)
                            .SendAsync("SelectionScoresChanged", new
                            {
                                BetId = @event.BetId,
                                SelectionId = @event.SelectionId,
                                HomeClubScore = @event.NewHomeClubScore,
                                AwayClubScore = @event.NewAwayClubScore
                            });
        }
    }
}
