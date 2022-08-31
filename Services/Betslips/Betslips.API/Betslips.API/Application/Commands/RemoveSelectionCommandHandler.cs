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
    public class RemoveSelectionCommandHandler : IRequestHandler<RemoveSelectionCommand, Betslip>
    {
        private readonly ILogger<RemoveSelectionCommandHandler> _logger;
        private readonly IBetslipRepository _repository;

        public RemoveSelectionCommandHandler(ILogger<RemoveSelectionCommandHandler> logger,
                                                IBetslipRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Betslip> Handle(RemoveSelectionCommand request, CancellationToken cancellationToken)
        {
            // Get the Betslip to AddSelection from the Database
            var betslip = await _repository.GetByGamblerIdAsync(request.GamblerId);

            // Remove the Selection using the provided class method of Betslip
            betslip.RemoveSelection(request.SelectionId);

            // Remove the Selection also from the Database
            _repository.RemoveSelectionById(request.SelectionId);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return betslip;
        }
    }
}
