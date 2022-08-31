using BettingApp.Services.Betting.API.Application.Commands;
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
    public class MatchResultsChangedDomainEventHandler : INotificationHandler<MatchResultsChangedDomainEvent>
    {
        private readonly ILogger<MatchResultsChangedDomainEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IMediator _mediator;

        public MatchResultsChangedDomainEventHandler(ILogger<MatchResultsChangedDomainEventHandler> logger,
                                                     IBetRepository repository,
                                                     IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(MatchResultsChangedDomainEvent matchChangedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A MatchResultsChangedDomainEvent is currently being handled...");

            // load Bet that contains the updated Match using the repository
            var bet = await _repository.GetByMatchIdAsync(matchChangedEvent.MatchId);

            // get the Selection to recalculate result
            var selection = bet.Selections
                                .Where(s => s.Match.Id.Equals(matchChangedEvent.MatchId))
                                .FirstOrDefault();

            _logger.LogInformation("A RecalculateSelectionResultCommand will be sent.");

            // create and send a RecalculateSelectionResultCommand using the mediator
            await _mediator.Send(new RecalculateSelectionResultCommand(selection.Id));
        }
    }
}
