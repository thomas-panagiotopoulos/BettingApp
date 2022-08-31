using BettingApp.Services.MatchSimulation.API.Application.Commands;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
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
    public class MatchNewCurrentMinuteCalculatedDomainEventHandler 
        : INotificationHandler<MatchNewCurrentMinuteCalculatedDomainEvent>
    {
        private readonly ILogger<MatchNewCurrentMinuteCalculatedDomainEventHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IMatchRepository _repository;

        public MatchNewCurrentMinuteCalculatedDomainEventHandler(
                                                    ILogger<MatchNewCurrentMinuteCalculatedDomainEventHandler> logger,
                                                    IMediator mediator,
                                                    IMatchRepository repository)
        {
            _logger = logger;
            _mediator = mediator;
            _repository = repository;
        }
        public async Task Handle(MatchNewCurrentMinuteCalculatedDomainEvent matchNewCurrentMinuteCalculatedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("An MatchNewCurrentMinuteCalculatedDomainEvent is currently being handled...");

            var updateMatchCurrentMinuteCommand = new UpdateMatchCurrentMinuteCommand(
                                                                matchNewCurrentMinuteCalculatedEvent.MatchId, 
                                                                matchNewCurrentMinuteCalculatedEvent.NewCurrentMinute);

            _logger.LogInformation("An UpdateMatchCurrentMinuteCommand will be sent.");

            await _mediator.Send(updateMatchCurrentMinuteCommand);
        }
    }
}
