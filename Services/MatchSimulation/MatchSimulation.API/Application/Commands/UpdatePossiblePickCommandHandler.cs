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
    public class UpdatePossiblePickCommandHandler : IRequestHandler<UpdatePossiblePickCommand, Match>
    {
        private readonly ILogger<UpdatePossiblePickCommandHandler> _logger;
        private readonly IMatchRepository _repository;

        public UpdatePossiblePickCommandHandler(ILogger<UpdatePossiblePickCommandHandler> logger,
                                                IMatchRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Match> Handle(UpdatePossiblePickCommand request, CancellationToken cancellationToken)
        {
            // load the Match the contains the PossiblePick from the repository
            var match = await _repository.GetByIdAsync(request.MatchId);

            // update the PossiblePick though the parent Match's provided method
            match.UpdatePossiblePick(request.MatchResultId, request.NewOdd, request.IsDisabled);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return match;
        }
    }
}
