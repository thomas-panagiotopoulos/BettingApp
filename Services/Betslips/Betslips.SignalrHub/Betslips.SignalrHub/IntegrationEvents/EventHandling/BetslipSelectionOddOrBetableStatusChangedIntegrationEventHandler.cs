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
    public class BetslipSelectionOddOrBetableStatusChangedIntegrationEventHandler
        : IIntegrationEventHandler<BetslipSelectionOddOrBetableStatusChangedIntegrationEvent>
    {
        private readonly ILogger<BetslipSelectionOddOrBetableStatusChangedIntegrationEventHandler> _logger;
        private readonly IHubContext<BetslipsHub> _hubContext;

        public BetslipSelectionOddOrBetableStatusChangedIntegrationEventHandler(
                                                        ILogger<BetslipSelectionOddOrBetableStatusChangedIntegrationEventHandler> logger,
                                                        IHubContext<BetslipsHub> hubContext)
        {
            _logger = logger;
            _hubContext = hubContext;
        }

        public async  Task Handle(BetslipSelectionOddOrBetableStatusChangedIntegrationEvent @event)
        {
            _logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

            await _hubContext.Clients
                            .Group(@event.GamblerId)
                            .SendAsync("SelectionOddOrBetableStatusChanged", new
                            {
                                BetslipId = @event.BetslipId,
                                SelectionId = @event.SelectionId,
                                Odd = @event.NewOdd,
                                IsBetable = @event.NewIsBetable
                            });
        }
    }
}
