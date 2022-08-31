using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Wallets.API.IntegrationEvents.Events;
using BettingApp.Services.Wallets.API.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.IntegrationEvents.EventHandling
{
    public class BetCanceledIntegrationEventHandler : IIntegrationEventHandler<BetCanceledIntegrationEvent>
    {
        private readonly ILogger<BetCanceledIntegrationEventHandler> _logger;
        private readonly IWalletsRepository _repository;
        private readonly IWalletsIntegrationEventService _walletsIntegrationEventService;

        public BetCanceledIntegrationEventHandler(ILogger<BetCanceledIntegrationEventHandler> logger,
                                                 IWalletsRepository repository,
                                                 IWalletsIntegrationEventService walletsIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _walletsIntegrationEventService = walletsIntegrationEventService;
        }

        public async Task Handle(BetCanceledIntegrationEvent @event)
        {
            // check if wallet exists, and if not then create one for the gambler
            var exists = _repository.WalletExistsWithGamblerId(@event.GamblerId);

            if (!exists)
            {
                _logger.LogInformation($"Wallet for Gambler: {@event.GamblerId} does not exist. A new Wallet will be created.");
                _repository.AddWallet(new Wallet(@event.GamblerId));
                await _repository.UnitOfWork.SaveChangesAsync();
            }

            // load the gambler's wallet
            var wallet = _repository.GetWalletByGamblerId(@event.GamblerId);

            // apply the transaction
            wallet.ApplyBetRefundTransaction(@event.WageredAmount, @event.BetId);

            // save changes
            await _repository.UnitOfWork.SaveChangesAsync();

            // publish UserWalletBalanceChangedIntegrationEvent
            var walletBalanceChangedIntegrationEvent = new WalletBalanceChangedIntegrationEvent(wallet.GamblerId, wallet.Balance, wallet.PreviousBalance);
            await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(walletBalanceChangedIntegrationEvent);
            await _walletsIntegrationEventService.PublishThroughEventBusAsync(walletBalanceChangedIntegrationEvent);

            // publish BetWageredAmountRefundedIntegrationEvent
            var betWageredAmountRefundedIntegrationEvent = new BetWageredAmountRefundedIntegrationEvent(
                                                                                @event.GamblerId, @event.BetId, 
                                                                                @event.WageredAmount, wallet.Balance);
            await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(betWageredAmountRefundedIntegrationEvent);
            await _walletsIntegrationEventService.PublishThroughEventBusAsync(betWageredAmountRefundedIntegrationEvent);
        }
    }
}
