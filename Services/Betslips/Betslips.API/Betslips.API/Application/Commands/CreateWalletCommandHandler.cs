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
    public class CreateWalletCommandHandler : IRequestHandler<CreateWalletCommand, Wallet>
    {
        private readonly ILogger<CreateWalletCommandHandler> _logger;
        private readonly IWalletRepository _repository;

        public CreateWalletCommandHandler(ILogger<CreateWalletCommandHandler> logger,
                                          IWalletRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Wallet> Handle(CreateWalletCommand request, CancellationToken cancellationToken)
        {
            // Create the new Wallet
            var wallet = new Wallet(request.GamblerId);

            // Add the new Wallet to the DB's Wallets table through the repository
            var addedWallet = _repository.Add(wallet);

            //SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return addedWallet;
        }
    }
}
