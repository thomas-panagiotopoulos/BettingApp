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
    public class CreateMatchCommandHandler : IRequestHandler<CreateMatchCommand, Match>
    {
        private readonly ILogger<CreateMatchCommandHandler> _logger;
        private readonly IMatchRepository _repository;

        public CreateMatchCommandHandler(ILogger<CreateMatchCommandHandler> logger,
                                         IMatchRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Match> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            // create the Match
            var match = new Match(request.MatchId, request.SimulationId, request.HomeClub, request.AwayClub, 
                                    request.LeagueId, request.KickoffDateTime, request.PossiblePicks);

            // add Match to the DB through the repository
            var addedMatch = _repository.Add(match);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return addedMatch;
        }
    }
}
