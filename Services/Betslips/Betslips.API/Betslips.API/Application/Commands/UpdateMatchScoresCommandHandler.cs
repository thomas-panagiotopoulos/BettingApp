using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Commands
{
    public class UpdateMatchScoresCommandHandler : IRequestHandler<UpdateMatchScoresCommand, bool>
    {
        private readonly ILogger<UpdateMatchScoresCommandHandler> _logger;
        private readonly IBetslipRepository _repository;

        public UpdateMatchScoresCommandHandler(ILogger<UpdateMatchScoresCommandHandler> logger,
                                                IBetslipRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateMatchScoresCommand request, CancellationToken cancellationToken)
        {
            // Load Betlip than contains the Match to update from the Database
            var betslip = await _repository.GetByMatchIdAsync(request.MatchId);

            // Update Match's scores through the provided method on parent Betslip
            betslip.UpdateMatchScores(request.MatchId, request.NewHomeClubScore, request.NewAwayClubScore);

            // SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
