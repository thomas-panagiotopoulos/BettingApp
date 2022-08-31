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
    public class ClearBetslipCommandHandler : IRequestHandler<ClearBetslipCommand, bool>
    {
        private readonly ILogger<ClearBetslipCommandHandler> _logger;
        private readonly IBetslipRepository _repository;

        public ClearBetslipCommandHandler(ILogger<ClearBetslipCommandHandler> logger,
                                            IBetslipRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(ClearBetslipCommand request, CancellationToken cancellationToken)
        {
            // Simply remove the Betslip from the Database
            var result = _repository.RemoveByGamblerId(request.GamblerId);

            // SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return result;
        }
    }
}
