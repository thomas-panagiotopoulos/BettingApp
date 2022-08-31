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
    public class RecalculateBetResultCommandHandler : IRequestHandler<RecalculateBetResultCommand, bool>
    {
        private readonly ILogger<RecalculateBetResultCommandHandler> _logger;
        private readonly IBetRepository _repository;

        public RecalculateBetResultCommandHandler(ILogger<RecalculateBetResultCommandHandler> logger,
                                                  IBetRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(RecalculateBetResultCommand request, CancellationToken cancellationToken)
        {
            // load Bet to recalculate result using the repository
            var bet = await _repository.GetAsync(request.BetId);

            // recalculate Bet's result through the provided class method
            bet.RecalculateBetResult();

            // SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
