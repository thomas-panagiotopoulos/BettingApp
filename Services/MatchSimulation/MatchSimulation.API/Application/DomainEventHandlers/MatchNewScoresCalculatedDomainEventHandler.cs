using BettingApp.Services.MatchSimulation.API.Application.Commands;
using BettingApp.Services.MatchSimulation.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.DomainEventHandlers
{
    public class MatchNewScoresCalculatedDomainEventHandler : INotificationHandler<MatchNewScoresCalculatedDomainEvent>
    {
        private readonly ILogger<MatchNewScoresCalculatedDomainEventHandler> _logger;
        private readonly IMediator _mediator;

        public MatchNewScoresCalculatedDomainEventHandler(ILogger<MatchNewScoresCalculatedDomainEventHandler> logger,
                                                          IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Handle(MatchNewScoresCalculatedDomainEvent matchNewScoresCalculatedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A MatchNewScoresCalculatedDomainEvent is currently being handled...");

            var updateMatchScoresCommand = new UpdateMatchScoresCommand(matchNewScoresCalculatedEvent.MatchId,
                                                                        matchNewScoresCalculatedEvent.NewHomeClubScore,
                                                                        matchNewScoresCalculatedEvent.NewAwayClubScore);

            _logger.LogInformation("An UpdateMatchScoresCommand will be sent.");

            await _mediator.Send(updateMatchScoresCommand);
        }
    }
}
