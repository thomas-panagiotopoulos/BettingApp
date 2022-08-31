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
    public class UpdateMatchCurrentMinuteCommandHandler : IRequestHandler<UpdateMatchCurrentMinuteCommand, bool>
    {
        private readonly ILogger<UpdateMatchCurrentMinuteCommandHandler> _logger;
        private readonly IBetRepository _repository;

        public UpdateMatchCurrentMinuteCommandHandler(ILogger<UpdateMatchCurrentMinuteCommandHandler> logger,
                                                      IBetRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateMatchCurrentMinuteCommand request, CancellationToken cancellationToken)
        {
            
            // load Bet that contains specific match using the repository
            var bet = await _repository.GetByMatchIdAsync(request.MatchId);

            // update the Match's current minute, through the method provided by aggregate root (Bet)
            // (this method may internally add a MatchStatusChangedDomainEvent to the queue)
            bet.UpdateMatchCurrentMinute(request.MatchId, request.NewCurrentMinute);

            // SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
