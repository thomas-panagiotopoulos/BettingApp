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
    public class BetslipBetableStatusChangedDomainEventHandler
        : INotificationHandler<BetslipBetableStatusChangedDomainEvent>
    {
        private readonly ILogger<BetslipBetableStatusChangedDomainEventHandler> _logger;
        private readonly IBetslipRepository _repository;
        private readonly IBetslipsIntegrationEventService _betslipsIntegrationEventService;

        public BetslipBetableStatusChangedDomainEventHandler(
                                            ILogger<BetslipBetableStatusChangedDomainEventHandler> logger,
                                            IBetslipRepository repository,
                                            IBetslipsIntegrationEventService betslipsIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _betslipsIntegrationEventService = betslipsIntegrationEventService;
        }

        public async Task Handle(BetslipBetableStatusChangedDomainEvent betslipStatusChangedEvent, CancellationToken cancellationToken)
        {
            var betslip = await _repository.GetByIdAsync(betslipStatusChangedEvent.BetslipId);

            var betslipBetableStatusChangedIntegrationEvent = new BetslipBetableStatusChangedIntegrationEvent(
                                                                                            betslip.GamblerId, betslip.Id, 
                                                                                            betslip.IsBetable);

            // Add a BetslipBetableStatusChangedIntegrationEvent to queue to be published latet
            // (receiver is the Betslips.SignalRHub service)
            await _betslipsIntegrationEventService.AddAndSaveEventAsync(betslipBetableStatusChangedIntegrationEvent);
        }
    }
}
