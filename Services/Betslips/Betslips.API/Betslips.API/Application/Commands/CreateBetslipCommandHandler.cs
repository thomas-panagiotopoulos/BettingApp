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
    public class CreateBetslipCommandHandler : IRequestHandler<CreateBetslipCommand, Betslip>
    {
        private readonly ILogger<CreateBetslipCommandHandler> _logger;
        private readonly IBetslipRepository _repository;

        public CreateBetslipCommandHandler(ILogger<CreateBetslipCommandHandler> logger,
                                            IBetslipRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Betslip> Handle(CreateBetslipCommand request, CancellationToken cancellationToken)
        {
            // Create the new Betslip
            var betslip = new Betslip(request.GamblerId);

            // Add the new Betslip to the DB's Betslips table through the repository
            var addedBetslip = _repository.Add(betslip);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return addedBetslip;
        }
    }
}
