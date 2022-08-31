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
    public class RemoveBetCommandHandler : IRequestHandler<RemoveBetCommand, bool>
    {
        private readonly ILogger<RemoveBetCommandHandler> _logger;
        private readonly IBetRepository _repository;

        public RemoveBetCommandHandler(ILogger<RemoveBetCommandHandler> logger, IBetRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(RemoveBetCommand request, CancellationToken cancellationToken)
        {
            // Simply remove the Bet from the Database
            var result = _repository.RemoveById(request.BetId);

            // SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return result;
        }
    }
}
