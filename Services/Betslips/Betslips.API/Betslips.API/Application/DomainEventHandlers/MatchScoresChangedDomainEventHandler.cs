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
    public class MatchScoresChangedDomainEventHandler : INotificationHandler<MatchScoresChangedDomainEvent>
    {
        private readonly ILogger<MatchScoresChangedDomainEventHandler> _logger;
        private readonly IBetslipRepository _repository;
        private readonly IBetslipsIntegrationEventService _betslipsIntegrationEventService;

        public MatchScoresChangedDomainEventHandler(
                                            ILogger<MatchScoresChangedDomainEventHandler> logger,
                                            IBetslipRepository repository,
                                            IBetslipsIntegrationEventService betslipsIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _betslipsIntegrationEventService = betslipsIntegrationEventService;
        }

        public async Task Handle(MatchScoresChangedDomainEvent matchScoresChanged, CancellationToken cancellationToken)
        {
            var betslip = await _repository.GetByMatchIdAsync(matchScoresChanged.MatchId);

            var selection = betslip.Selections.FirstOrDefault(s => s.Match.Id.Equals(matchScoresChanged.MatchId));

            var selectionMatchScoresChangedIntegrationEvent = new BetslipSelectionMatchScoresChangedIntegrationEvent(
                                                                                          betslip.GamblerId, betslip.Id, 
                                                                                          selection.Id,
                                                                                          selection.Match.HomeClubScore,
                                                                                          selection.Match.AwayClubScore);

            // Add a SelectionMatchScoresChangedIntegrationEvent to queue to be published latet
            // (receiver is the Betslips.SignalRHub service)
            await _betslipsIntegrationEventService.AddAndSaveEventAsync(selectionMatchScoresChangedIntegrationEvent);
        }
    }
}
