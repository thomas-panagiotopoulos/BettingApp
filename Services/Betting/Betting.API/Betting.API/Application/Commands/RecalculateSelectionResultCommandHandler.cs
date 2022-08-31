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
    public class RecalculateSelectionResultCommandHandler : IRequestHandler<RecalculateSelectionResultCommand, bool>
    {
        private readonly ILogger<RecalculateSelectionResultCommandHandler> _logger;
        private readonly IBetRepository _repository;

        public RecalculateSelectionResultCommandHandler(ILogger<RecalculateSelectionResultCommandHandler> logger,
                                                        IBetRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(RecalculateSelectionResultCommand request, CancellationToken cancellationToken)
        {
            // load Bet that contains specific Selection to recalculate result using the repository
            var bet = await _repository.GetBySelectionIdAsync(request.SelectionId);

            // recalculate Selection's result, through the method provided by aggregate root (Bet)
            // (this method may internally add a SelectionResultChangedDomainEvent to the queue)
            bet.RecalculateSelectionResult(request.SelectionId);

            // SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
