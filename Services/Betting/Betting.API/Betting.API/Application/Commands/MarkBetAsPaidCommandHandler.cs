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
    public class MarkBetAsPaidCommandHandler : IRequestHandler<MarkBetAsPaidCommand, bool>
    {
        private readonly ILogger<MarkBetAsPaidCommandHandler> _logger;
        private readonly IBetRepository _repository;

        public MarkBetAsPaidCommandHandler(ILogger<MarkBetAsPaidCommandHandler> logger, IBetRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(MarkBetAsPaidCommand request, CancellationToken cancellationToken)
        {
            // load specific Bet using the repository
            var bet = await _repository.GetAsync(request.BetId);

            // set isPaid to true
            bet.MarkBetAsPaid();

            // SaveEntitiesAsync
            // SaveEntitiesAsync (dispatch Domain and Integration events and the SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
