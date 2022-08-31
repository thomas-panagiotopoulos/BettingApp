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
    public class CancelBetCommandHandler : IRequestHandler<CancelBetCommand, bool>
    {
        private readonly ILogger<CancelBetCommandHandler> _logger;
        private readonly IBetRepository _repository;

        public CancelBetCommandHandler(ILogger<CancelBetCommandHandler> logger,IBetRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(CancelBetCommand request, CancellationToken cancellationToken)
        {

            // load specific Bet using the repository
            var bet = await _repository.GetAsync(request.BetId);

            // set Bet status to Canceled (this method internally adds a BetCanceledDomainEvent to the queue)
            bet.CancelBetByUserRequest();

            // SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
