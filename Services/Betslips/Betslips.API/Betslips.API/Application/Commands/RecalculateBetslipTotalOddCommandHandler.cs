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
    public class RecalculateBetslipTotalOddCommandHandler : IRequestHandler<RecalculateBetslipTotalOddCommand, bool>
    {
        private readonly ILogger<RecalculateBetslipTotalOddCommandHandler> _logger;
        private readonly IBetslipRepository _repository;

        public RecalculateBetslipTotalOddCommandHandler(ILogger<RecalculateBetslipTotalOddCommandHandler> logger,
                                                    IBetslipRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(RecalculateBetslipTotalOddCommand request, CancellationToken cancellationToken)
        {
            // Get the Betslip to update from the Database
            var betslip = await _repository.GetByIdAsync(request.BetslipId);

            // Recalculate the TotalOdd through the provided class method
            betslip.RecalculateBetslipTotalOdd();

            // SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
