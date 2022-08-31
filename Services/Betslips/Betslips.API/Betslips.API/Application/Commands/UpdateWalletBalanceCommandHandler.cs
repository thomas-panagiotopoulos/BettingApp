using BettingApp.Services.Betslips.Domain.AggregatesModel.WalletAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Commands
{
    public class UpdateWalletBalanceCommandHandler : IRequestHandler<UpdateWalletBalanceCommand, bool>
    {
        private readonly ILogger<UpdateWalletBalanceCommandHandler> _logger;
        private readonly IWalletRepository _repository;

        public UpdateWalletBalanceCommandHandler(ILogger<UpdateWalletBalanceCommandHandler> logger,
                                                 IWalletRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateWalletBalanceCommand request, CancellationToken cancellationToken)
        {
            // Get the Wallet to update from the repository
            var wallet = await _repository.GetByGamblerIdAsync(request.GamblerId);

            // Update Wallet's Balance using the provided class method
            wallet.UpdateBalance(request.NewBalance, request.OldBalance);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return true;
        }
    }
}
