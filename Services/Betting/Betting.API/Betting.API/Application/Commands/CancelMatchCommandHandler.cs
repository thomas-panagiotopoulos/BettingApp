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
    public class CancelMatchCommandHandler : IRequestHandler<CancelMatchCommand, bool>
    {
        private readonly ILogger<CancelMatchCommandHandler> _logger;
        private readonly IBetRepository _repository;

        public CancelMatchCommandHandler(ILogger<CancelMatchCommandHandler> logger,
                                         IBetRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(CancelMatchCommand request, CancellationToken cancellationToken)
        {
            // load Bet that contains specific match using the repository
            var bet = await _repository.GetByMatchIdAsync(request.MatchId);

            // cancel the Match, through the method provided by aggregate root (Bet)
            // (this method may internally add a MatchStatusChangedDomainEvent to the queue)
            bet.CancelMatch(request.MatchId);

            // SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
