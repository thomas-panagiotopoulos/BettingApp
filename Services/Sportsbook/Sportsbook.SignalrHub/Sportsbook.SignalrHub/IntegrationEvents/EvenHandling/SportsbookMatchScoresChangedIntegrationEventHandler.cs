using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Sportsbook.SignalrHub.IntegrationEvents.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportsbook.SignalrHub.IntegrationEvents.EvenHandling
{
    public class SportsbookMatchScoresChangedIntegrationEventHandler : IIntegrationEventHandler<SportsbookMatchScoresChangedIntegrationEvent>
    {
        private readonly ILogger<SportsbookMatchScoresChangedIntegrationEventHandler> _logger;
        private readonly IHubContext<SportsbookHub> _hubContext;

        public SportsbookMatchScoresChangedIntegrationEventHandler(
                                                        ILogger<SportsbookMatchScoresChangedIntegrationEventHandler> logger,
                                                        IHubContext<SportsbookHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task Handle(SportsbookMatchScoresChangedIntegrationEvent @event)
        {
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            await _hubContext.Clients
                            .All
                            .SendAsync("MatchScoresChanged", new { MatchId = @event.MatchId, 
                                                                   HomeClubScore = @event.NewHomeClubScore,
                                                                   AwayClubScore = @event.NewAwayClubScore });
        }
    }
}
