using BettingApp.Services.Betslips.API.Application.IntegrationEvents;
using BettingApp.Services.Betslips.API.Application.IntegrationEvents.Events.SignalRHub;
using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using BettingApp.Services.Betslips.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.DomainEventHandlers
{
    public class BetslipDetailsChangedDomainEventHandler : INotificationHandler<BetslipDetailsChangedDomainEvent>
    {
        private readonly ILogger<BetslipDetailsChangedDomainEventHandler> _logger;
        private readonly IBetslipRepository _repository;
        private readonly IBetslipsIntegrationEventService _betslipsIntegrationEventService;

        public BetslipDetailsChangedDomainEventHandler(
                                            ILogger<BetslipDetailsChangedDomainEventHandler> logger,
                                            IBetslipRepository repository,
                                            IBetslipsIntegrationEventService betslipsIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _betslipsIntegrationEventService = betslipsIntegrationEventService;
        }

        public async Task Handle(BetslipDetailsChangedDomainEvent betslipDetailsChangedEvent, CancellationToken cancellationToken)
        {
            var betslip = await _repository.GetByIdAsync(betslipDetailsChangedEvent.BetslipId);

            var betslipDetailsChangedIntegrationEvent = new BetslipDetailsChangedIntegrationEvent(betslip.GamblerId,
                                                                                betslip.Id, betslip.TotalOdd, betslip.PotentialWinnings,
                                                                                betslip.PotentialProfit, betslip.WageredAmount);

            // Add a BetslipDetailsChangedIntegrationEvent to queue to be published latet
            // (receiver is the Betslips.SignalRHub service)
            await _betslipsIntegrationEventService.AddAndSaveEventAsync(betslipDetailsChangedIntegrationEvent);
        }
    }
}
