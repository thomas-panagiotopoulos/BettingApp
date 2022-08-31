using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.Commands
{
    public class CancelMatchCommandHandler : IRequestHandler<CancelMatchCommand, Match>
    {
        private readonly ILogger<CancelMatchCommandHandler> _logger;
        private readonly IMatchRepository _repository;

        public CancelMatchCommandHandler(ILogger<CancelMatchCommandHandler> logger,
                                         IMatchRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<Match> Handle(CancelMatchCommand request, CancellationToken cancellationToken)
        {
            // Load Match from the repository
            var match = await _repository.GetByIdAsync(request.MatchId);

            // Cancel Match by provided class method (internally adds Domain Events)
            match.Cancel();

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return match;
        }
    }
}
