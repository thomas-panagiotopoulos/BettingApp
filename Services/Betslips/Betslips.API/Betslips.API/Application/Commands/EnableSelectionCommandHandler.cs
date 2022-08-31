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
    public class EnableSelectionCommandHandler : IRequestHandler<EnableSelectionCommand, bool>
    {
        private readonly ILogger<EnableSelectionCommandHandler> _logger;
        private readonly IBetslipRepository _repository;
        private readonly IMediator _mediator;

        public EnableSelectionCommandHandler(ILogger<EnableSelectionCommandHandler> logger,
                                              IBetslipRepository repository,
                                              IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(EnableSelectionCommand request, CancellationToken cancellationToken)
        {
            // Get the Betslip that contains the Selection to update from the Database
            var betslip = await _repository.GetBySelectionIdAsync(request.SelectionId);

            // Disable the Selection through the provided method on parent Betslip
            betslip.EnableSelection(request.SelectionId);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
