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
    public class UpdateMatchCurrentMinuteCommandHandler : IRequestHandler<UpdateMatchCurrentMinuteCommand, bool>
    {
        private readonly ILogger<UpdateMatchCurrentMinuteCommandHandler> _logger;
        private readonly IBetslipRepository _repository;

        public UpdateMatchCurrentMinuteCommandHandler(ILogger<UpdateMatchCurrentMinuteCommandHandler> logger,
                                                        IBetslipRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateMatchCurrentMinuteCommand request, CancellationToken cancellationToken)
        {
            //Get the Betslip that contains specific Match from the Database
            var betslip = await _repository.GetByMatchIdAsync(request.MatchId);

            //Update Match's current minute through the provided method on patent Betslip
            betslip.UpdateMatchCurrentMinute(request.MatchId, request.NewCurrentMinute);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
