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
    public class UpdateMatchCurrentMinuteCommandHandler : IRequestHandler<UpdateMatchCurrentMinuteCommand, Match>
    {
        private readonly ILogger<UpdateMatchCurrentMinuteCommandHandler> _logger;
        private readonly IMatchRepository _repository;

        public UpdateMatchCurrentMinuteCommandHandler(ILogger<UpdateMatchCurrentMinuteCommandHandler> logger,
                                                      IMatchRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<Match> Handle(UpdateMatchCurrentMinuteCommand request, CancellationToken cancellationToken)
        {
            // load the Match to update from the repository
            var match = await _repository.GetByIdAsync(request.MatchId);

            // update Match's current minute through the provided class method
            match.UpdateCurrentMinute(request.NewCurrentMinute);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return match;
        }
    }
}
