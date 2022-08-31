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
    public class UpdateMatchScoresCommandHandler : IRequestHandler<UpdateMatchScoresCommand, Match>
    {
        private readonly ILogger<UpdateMatchScoresCommandHandler> _logger;
        private readonly IMatchRepository _repository;

        public UpdateMatchScoresCommandHandler(ILogger<UpdateMatchScoresCommandHandler> logger,
                                               IMatchRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<Match> Handle(UpdateMatchScoresCommand request, CancellationToken cancellationToken)
        {
            // load the Match to update from the repository
            var match = await _repository.GetByIdAsync(request.MatchId);

            // update the Match's scores through the provided class method
            match.UpdateScores(request.NewHomeClubScore, request.NewAwayClubScore);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return match;
        }
    }
}
