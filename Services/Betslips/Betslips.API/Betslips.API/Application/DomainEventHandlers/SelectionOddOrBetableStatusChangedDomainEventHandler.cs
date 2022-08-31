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
    public class SelectionOddOrBetableStatusChangedDomainEventHandler
        : INotificationHandler<SelectionOddOrBetableStatusChangedDomainEvent>
    {
        private readonly ILogger<SelectionOddOrBetableStatusChangedDomainEventHandler> _logger;
        private readonly IBetslipRepository _repository;
        private readonly IBetslipsIntegrationEventService _betslipsIntegrationEventService;

        public SelectionOddOrBetableStatusChangedDomainEventHandler(
                                            ILogger<SelectionOddOrBetableStatusChangedDomainEventHandler> logger,
                                            IBetslipRepository repository,
                                            IBetslipsIntegrationEventService betslipsIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _betslipsIntegrationEventService = betslipsIntegrationEventService;
        }

        public async Task Handle(SelectionOddOrBetableStatusChangedDomainEvent selectionChangedEvent, CancellationToken cancellationToken)
        {
            var betslip = await _repository.GetBySelectionIdAsync(selectionChangedEvent.SelectionId);

            var selection = betslip.Selections.FirstOrDefault(s => s.Id.Equals(selectionChangedEvent.SelectionId));

            var selectionOddOrBetableStatusChangedIntegrationEvent = 
                new BetslipSelectionOddOrBetableStatusChangedIntegrationEvent(betslip.GamblerId, betslip.Id, selection.Id, 
                                                                                selection.Odd, selection.IsBetable);

            // Add a SelectionOddOrBetableStatusChangedIntegrationEvent to queue to be published latet
            // (receiver is the Betslips.SignalRHub service)
            await _betslipsIntegrationEventService.AddAndSaveEventAsync(selectionOddOrBetableStatusChangedIntegrationEvent);
        }
    }
}
