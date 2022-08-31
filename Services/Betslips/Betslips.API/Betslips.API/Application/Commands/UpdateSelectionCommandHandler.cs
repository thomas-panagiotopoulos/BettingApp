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
    public class UpdateSelectionCommandHandler : IRequestHandler<UpdateSelectionCommand, bool>
    {
        private readonly ILogger<UpdateSelectionCommandHandler> _logger;
        private readonly IBetslipRepository _repository;

        public UpdateSelectionCommandHandler(ILogger<UpdateSelectionCommandHandler> logger,
                                                IBetslipRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateSelectionCommand request, CancellationToken cancellationToken)
        {
            // Get the Betslip that contains the Selection to update from the Database
            var betslip = await _repository.GetBySelectionIdAsync(request.SelectionId);

            // Update the Selection's odd through the provided method on parent Betslip
            betslip.UpdateSelection(request.SelectionId, request.NewOdd, request.IsDisabled);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
