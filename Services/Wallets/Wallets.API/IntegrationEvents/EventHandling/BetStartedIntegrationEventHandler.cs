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
    public class BetStartedIntegrationEventHandler : IIntegrationEventHandler<BetStartedIntegrationEvent>
    {
        private readonly ILogger<BetStartedIntegrationEventHandler> _logger;
        private readonly IWalletsRepository _repository;
        private readonly IWalletsIntegrationEventService _walletsIntegrationEventService;

        public BetStartedIntegrationEventHandler(ILogger<BetStartedIntegrationEventHandler> logger,
                                                 IWalletsRepository repository,
                                                 IWalletsIntegrationEventService walletsIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _walletsIntegrationEventService = walletsIntegrationEventService;
        }

        public async Task Handle(BetStartedIntegrationEvent @event)
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

            // check if wallet's balance is insufficient
            if (wallet.Balance < @event.WageredAmount)
            {
                _logger.LogInformation($"Payment for Bet: {@event.BetId} failed. Insufficient wallet balance.");

                var paymentFailedIntegrationEvent = new BetPaymentFailedIntegrationEvent(@event.GamblerId, @event.BetId);
                await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(paymentFailedIntegrationEvent);
                await _walletsIntegrationEventService.PublishThroughEventBusAsync(paymentFailedIntegrationEvent);

                return;
            }

            // apply the transaction
            wallet.ApplyBetPaymentTransaction(@event.WageredAmount, @event.BetId);

            // save changes
            await _repository.UnitOfWork.SaveChangesAsync();

            // publish BetPaymentSucceededIntegrationEvent
            var betPaymentSucceededIntegrationEvent = new BetPaymentSucceededIntegrationEvent(@event.GamblerId, @event.BetId);
            await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(betPaymentSucceededIntegrationEvent);
            await _walletsIntegrationEventService.PublishThroughEventBusAsync(betPaymentSucceededIntegrationEvent);

            // publish UserWalletBalanceChangedIntegrationEvent
            var walletBalanceChangedIntegrationEvent = new WalletBalanceChangedIntegrationEvent(wallet.GamblerId, wallet.Balance, wallet.PreviousBalance);
            await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(walletBalanceChangedIntegrationEvent);
            await _walletsIntegrationEventService.PublishThroughEventBusAsync(walletBalanceChangedIntegrationEvent);
        }
    }
}
