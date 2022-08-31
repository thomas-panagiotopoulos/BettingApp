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
    public class RecalculateBetslipBetableStatusCommandHandler
        : IRequestHandler<RecalculateBetslipBetableStatusCommand, bool>
    {
        private readonly ILogger<RecalculateBetslipBetableStatusCommandHandler> _logger;
        private readonly IBetslipRepository _repository;

        public RecalculateBetslipBetableStatusCommandHandler(
                                        ILogger<RecalculateBetslipBetableStatusCommandHandler> logger,
                                        IBetslipRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(RecalculateBetslipBetableStatusCommand request, CancellationToken cancellationToken)
        {
            // Load Betslip to update from the Database
            var betslip = await _repository.GetByIdAsync(request.BetslipId);

            // Recalculate Betslip's Betable status through the provided class method
            betslip.RecalculateBetslipBetableStatus();

            // SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
