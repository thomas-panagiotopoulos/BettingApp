using BettingApp.Services.Betting.API.Application.IntegrationEvents;
using BettingApp.Services.Betting.API.Application.IntegrationEvents.Events.SignalRHub;
using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using BettingApp.Services.Betting.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.DomainEventHandlers
{
    public class MatchCanceledDomainEventHandler : INotificationHandler<MatchCanceledDomainEvent>
    {
        private readonly ILogger<MatchCanceledDomainEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IBettingIntegrationEventService _bettingIntgrationEventService;

        public MatchCanceledDomainEventHandler(ILogger<MatchCanceledDomainEventHandler> logger,
                                             IBetRepository repository,
                                             IBettingIntegrationEventService bettingIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _bettingIntgrationEventService = bettingIntegrationEventService;
        }

        public async Task Handle(MatchCanceledDomainEvent matchCanceledEvent, CancellationToken cancellationToken)
        {
            var bet = await _repository.GetByMatchIdAsync(matchCanceledEvent.MatchId);

            var selection = bet.Selections.FirstOrDefault(s => s.Match.Id.Equals(matchCanceledEvent.MatchId));

            var betSelectionMatchCanceledIntegrationEvent = new BetSelectionMatchCanceledIntegrationEvent(bet.GamblerId, 
                                                                                                    bet.Id, selection.Id);

            // Add a BetSelectionMatchCanceledIntegrationEvent to queue to be published latet
            // (receiver is the Betting.SignalRHub service)
            await _bettingIntgrationEventService.AddAndSaveEventAsync(betSelectionMatchCanceledIntegrationEvent);
        }
    }
}
