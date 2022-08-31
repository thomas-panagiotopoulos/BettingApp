using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Betslips.SignalrHub.IntegrationEvents.Events;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.SignalrHub.IntegrationEvents.EventHandling
{
    public class BetslipDetailsChangedIntegrationEventHandler
        : IIntegrationEventHandler<BetslipDetailsChangedIntegrationEvent>
    {
        private readonly ILogger<BetslipDetailsChangedIntegrationEventHandler> _logger;
        private readonly IHubContext<BetslipsHub> _hubContext;

        public BetslipDetailsChangedIntegrationEventHandler(
                                                    ILogger<BetslipDetailsChangedIntegrationEventHandler> logger,
                                                    IHubContext<BetslipsHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public async Task Handle(BetslipDetailsChangedIntegrationEvent @event)
        {
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            await _hubContext.Clients
                            .Group(@event.GamblerId)
                            .SendAsync("BetslipDetailsChanged", new
                            {
                                BetslipId = @event.BetslipId,
                                TotalOdd = @event.NewTotalOdd,
                                PotentialWinnings = @event.NewPotentialWinnings,
                                PotentialProfit = @event.NewPotentialProfit,
                                WageredAmount = @event.NewWageredAmount
                            });
        }
    }
}
