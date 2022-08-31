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
    public class RecalculateBetStatusCommandHandler : IRequestHandler<RecalculateBetStatusCommand, bool>
    {
        private readonly ILogger<RecalculateBetStatusCommandHandler> _logger;
        private readonly IBetRepository _repository;

        public RecalculateBetStatusCommandHandler(ILogger<RecalculateBetStatusCommandHandler> logger,
                                                  IBetRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(RecalculateBetStatusCommand request, CancellationToken cancellationToken)
        {
            // load Bet to recalculate status using the repository
            var bet = await _repository.GetAsync(request.BetId);

            // recalculate Bet's status through the provided class method
            // (this method may internally add a BetCompletedDomainEvent or a BetCanceledDomainEvent
            // to the queue)
            bet.RecalculateBetStatus();

            // SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
