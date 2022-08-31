using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.Commands
{
    public class RecalculateSelectionStatusCommandHandler : IRequestHandler<RecalculateSelectionStatusCommand, bool>
    {
        private readonly ILogger<RecalculateSelectionStatusCommandHandler> _logger;
        private readonly IBetRepository _repository;

        public RecalculateSelectionStatusCommandHandler(ILogger<RecalculateSelectionStatusCommandHandler> logger,
                                                        IBetRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(RecalculateSelectionStatusCommand request, CancellationToken cancellationToken)
        {
            // load Bet that contains specific Selection to recalculate status using the repository
            var bet = await _repository.GetBySelectionIdAsync(request.SelectionId);

            // recalculate Selection's status, through the method provided by aggregate root (Bet)
            // (this method may internally add a SelectionStatusChangedDomainEvent to the queue)
            bet.RecalculateSelectionStatus(request.SelectionId);

            // SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;

        }
    }
}
