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
    public class CancelMatchCommandHandler : IRequestHandler<CancelMatchCommand, bool>
    {
        private readonly ILogger<CancelMatchCommandHandler> _logger;
        private readonly IBetslipRepository _repository;

        public CancelMatchCommandHandler(ILogger<CancelMatchCommandHandler> logger,
                                         IBetslipRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(CancelMatchCommand request, CancellationToken cancellationToken)
        {
            // Get the Betslip that contains the Match to cancel from the Database
            var betslip = await _repository.GetByMatchIdAsync(request.MatchId);

            // Cancel the Match though the provided class method on parent Betslip
            betslip.CancelMatch(request.MatchId);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
