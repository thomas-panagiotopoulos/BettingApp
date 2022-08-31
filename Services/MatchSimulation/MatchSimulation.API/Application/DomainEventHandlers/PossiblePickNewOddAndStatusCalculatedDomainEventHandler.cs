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
    public class PossiblePickNewOddAndStatusCalculatedDomainEventHandler
        : INotificationHandler<PossiblePickNewOddAndStatusCalculatedDomainEvent>
    {
        private readonly ILogger<PossiblePickNewOddAndStatusCalculatedDomainEventHandler> _logger;
        private readonly IMediator _mediator;

        public PossiblePickNewOddAndStatusCalculatedDomainEventHandler(
                                                ILogger<PossiblePickNewOddAndStatusCalculatedDomainEventHandler> logger,
                                                IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task Handle(PossiblePickNewOddAndStatusCalculatedDomainEvent possiblePickNewValuesCalculatedEvent, CancellationToken cancellationToken)
        {
            _logger.LogInformation("A PossiblePickNewOddAndStatusCalculatedDomainEvent is currently being handled...");

            // create and send an UpdatePossiblePickCommand to update the related PossiblePick with its new values
            // (Odd and IsDisabled status)
            var updatePossiblePickCommand = new UpdatePossiblePickCommand(
                                                            possiblePickNewValuesCalculatedEvent.MatchId,
                                                            possiblePickNewValuesCalculatedEvent.MatchResultId,
                                                            possiblePickNewValuesCalculatedEvent.NewOdd,
                                                            possiblePickNewValuesCalculatedEvent.IsExtremeValue == true);

            _logger.LogInformation("A UpdatePossiblePickCommand will be sent.");

            await _mediator.Send(updatePossiblePickCommand);
        }
    }
}
