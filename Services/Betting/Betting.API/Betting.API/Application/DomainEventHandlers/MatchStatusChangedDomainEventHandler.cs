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
    public class MatchStatusChangedDomainEventHandler : INotificationHandler<MatchStatusChangedDomainEvent>
    {
        private readonly ILogger<MatchStatusChangedDomainEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IMediator _mediator;

        public MatchStatusChangedDomainEventHandler(ILogger<MatchStatusChangedDomainEventHandler> logger,
                                                    IBetRepository repository,
                                                    IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(MatchStatusChangedDomainEvent matchChangedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A MatchStatusChangedDomainEvent is currently being handled...");

            // load Bet that contains the updated Match using the repository
            var bet = await _repository.GetByMatchIdAsync(matchChangedEvent.MatchId);

            // get the Selection to recalculate status
            var selection = bet.Selections
                                .Where(s => s.Match.Id.Equals(matchChangedEvent.MatchId))
                                .FirstOrDefault();

            _logger.LogInformation("A RecalculateSelectionStatusCommand will be sent.");

            // create and send a RecalculateSelectionStatusCommand using the mediator
            await _mediator.Send(new RecalculateSelectionStatusCommand(selection.Id));

        }
    }
}
