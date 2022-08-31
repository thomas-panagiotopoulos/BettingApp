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
    public class AddSelectionCommandHandler : IRequestHandler<AddSelectionCommand, Betslip>
    {
        private readonly ILogger<AddSelectionCommandHandler> _logger;
        private readonly IBetslipRepository _repository;

        public AddSelectionCommandHandler(ILogger<AddSelectionCommandHandler> logger,
                                            IBetslipRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Betslip> Handle(AddSelectionCommand request, CancellationToken cancellationToken)
        {
            // Get the Betslip to AddSelection from the Database
            var betslip = await _repository.GetByGamblerIdAsync(request.GamblerId);

            // Add the new Selection using the provided class method of Betslip
            betslip.AddSelection(request.GamblerMatchResultId, request.Odd, request.InitialOdd, request.RelatedMatchId,
                                 request.HomeClubName, request.AwayClubName, request.KickoffDateTime, 
                                 request.CurrentMinute, request.HomeClubScore, request.AwayClubScore, 
                                 request.RequirementTypeId, request.RequiredValue, request.LatestAdditionId);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return betslip;
        }
    }
}
