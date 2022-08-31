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
    public class MatchCurrentMinuteChangedDomainEventHandler : INotificationHandler<MatchCurrentMinuteChangedDomainEvent>
    {
        private readonly ILogger<MatchCurrentMinuteChangedDomainEventHandler> _logger;
        private readonly IBetslipRepository _repository;
        private readonly IBetslipsIntegrationEventService _betslipsIntegrationEventService;

        public MatchCurrentMinuteChangedDomainEventHandler(
                                            ILogger<MatchCurrentMinuteChangedDomainEventHandler> logger,
                                            IBetslipRepository repository,
                                            IBetslipsIntegrationEventService betslipsIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _betslipsIntegrationEventService = betslipsIntegrationEventService;
        }

        public async Task Handle(MatchCurrentMinuteChangedDomainEvent matchMinuteChangedEvent, CancellationToken cancellationToken)
        {
            var betslip = await _repository.GetByMatchIdAsync(matchMinuteChangedEvent.MatchId);

            var selection = betslip.Selections.FirstOrDefault(s => s.Match.Id.Equals(matchMinuteChangedEvent.MatchId));

            var selectionMatchCurrentMinuteChangedIntegrationEvent = 
                            new BetslipSelectionMatchCurrentMinuteChangedIntegrationEvent(betslip.GamblerId, betslip.Id, 
                                                                          selection.Id, selection.Match.CurrentMinute);

            // Add a SelectionMatchCurrentMinuteChangedIntegrationEvent to queue to be published latet
            // (receiver is the Betslips.SignalRHub service)
            await _betslipsIntegrationEventService.AddAndSaveEventAsync(selectionMatchCurrentMinuteChangedIntegrationEvent);
        }
    }
}
