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
    public class UpdateWageredAmountCommandHandler : IRequestHandler<UpdateWageredAmountCommand, Betslip>
    {
        private readonly ILogger<UpdateWageredAmountCommandHandler> _logger;
        private readonly IBetslipRepository _repository;

        public UpdateWageredAmountCommandHandler(ILogger<UpdateWageredAmountCommandHandler> logger,
                                                IBetslipRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<Betslip> Handle(UpdateWageredAmountCommand request, CancellationToken cancellationToken)
        {
            // Get the Betslip to AddSelection from the Database
            var betslip = await _repository.GetByGamblerIdAsync(request.GamblerId);

            // Update the Wagered Amount using the provided class method of Betslip
            betslip.UpdateWageredAmount(request.NewWageredAmount);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return betslip;
        }
    }
}
