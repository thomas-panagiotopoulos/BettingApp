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
    public class RecalculateSelectionBetableStatusCommandHandler
        : IRequestHandler<RecalculateSelectionBetableStatusCommand, bool>
    {
        private readonly ILogger<RecalculateSelectionBetableStatusCommandHandler> _logger;
        private readonly IBetslipRepository _repository;

        public RecalculateSelectionBetableStatusCommandHandler(
                                         ILogger<RecalculateSelectionBetableStatusCommandHandler> logger,
                                         IBetslipRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(RecalculateSelectionBetableStatusCommand request, CancellationToken cancellationToken)
        {
            // Get the Betslip that contains the Selection to update from the Database
            var betslip = await _repository.GetBySelectionIdAsync(request.SelectionId);

            // Recalculate Selections's Betable status through the class method provided by parent Betslip
            betslip.ReclaculateSelectionBetableStatus(request.SelectionId);

            // SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
