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
    public class BetSelectionMatchCurrentMinuteChangedIntegrationEventHandler
        : IIntegrationEventHandler<BetSelectionMatchCurrentMinuteChangedIntegrationEvent>
    {
        private readonly ILogger<BetSelectionMatchCurrentMinuteChangedIntegrationEventHandler> _logger;
        private readonly IHubContext<BettingHub> _hubContext;

        public BetSelectionMatchCurrentMinuteChangedIntegrationEventHandler(
                                                        ILogger<BetSelectionMatchCurrentMinuteChangedIntegrationEventHandler> logger,
                                                        IHubContext<BettingHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task Handle(BetSelectionMatchCurrentMinuteChangedIntegrationEvent @event)
        {
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            await _hubContext.Clients
                            .Group(@event.GamblerId)
                            .SendAsync("SelectionCurrentMinuteChanged", new 
                            { 
                                BetId = @event.BetId,
                                SelectionId = @event.SelectionId,
                                CurrentMinute = @event.NewCurrentMinute
                            });
        }
    }
}
