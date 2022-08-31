using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.Commands
{
    public class CancelBetDueToRejectedPaymentCommandHandler : IRequestHandler<CancelBetDueToRejectedPaymentCommand, bool>
    {
        private readonly ILogger<CancelBetDueToRejectedPaymentCommandHandler> _logger;
        private readonly IBetRepository _repository;

        public CancelBetDueToRejectedPaymentCommandHandler(ILogger<CancelBetDueToRejectedPaymentCommandHandler> logger,
                                                           IBetRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(CancelBetDueToRejectedPaymentCommand request, CancellationToken cancellationToken)
        {
            // load specific Bet using the repository
            var bet = await _repository.GetAsync(request.BetId);

            // set Bet status to Canceled
            // (this method internally adds a BetCanceledDueToRejectedPaymentDomainEvent to the queue)
            bet.CancelBetWhenPaymentIsRejected();

            // SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
