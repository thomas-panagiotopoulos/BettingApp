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
    public class WithdrawAcceptedByBankIntegrationEventHandler : IIntegrationEventHandler<WithdrawAcceptedByBankIntegrationEvent>
    {
        private readonly ILogger<WithdrawAcceptedByBankIntegrationEventHandler> _logger;
        private readonly IWalletsRepository _repository;
        private readonly IWalletsIntegrationEventService _walletsIntegrationEventService;

        public WithdrawAcceptedByBankIntegrationEventHandler(ILogger<WithdrawAcceptedByBankIntegrationEventHandler> logger,
                                                 IWalletsRepository repository,
                                                 IWalletsIntegrationEventService walletsIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _walletsIntegrationEventService = walletsIntegrationEventService;
        }

        public async Task Handle(WithdrawAcceptedByBankIntegrationEvent @event)
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

            try
            {
                // apply the transaction
                wallet.ApplyWithdrawTransaction(@event.Amount, @event.RequestId);
            }
            catch (Exception e)
            {
                _logger.LogInformation($"Exception: {e.Message}");

                // publish ApplyWithdrawFailedIntegrationEvent (for Payment)
                var applyWithdrawFailedIntegrationEvent = new ApplyWithdrawFailedIntegrationEvent(@event.GamblerId, @event.Amount, @event.RequestId);
                await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(applyWithdrawFailedIntegrationEvent);
                await _walletsIntegrationEventService.PublishThroughEventBusAsync(applyWithdrawFailedIntegrationEvent);

                return;
            }

            // save changes
            await _repository.UnitOfWork.SaveChangesAsync();

            // publish UserWalletBalanceChangedIntegrationEvent (for Betslips)
            var walletBalanceChangedIntegrationEvent = new WalletBalanceChangedIntegrationEvent(wallet.GamblerId, wallet.Balance, wallet.PreviousBalance);
            await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(walletBalanceChangedIntegrationEvent);
            await _walletsIntegrationEventService.PublishThroughEventBusAsync(walletBalanceChangedIntegrationEvent);

            // publish ApplyWithdrawSucceededIntegrationEvent (for Payment)
            var applyWithdrawSucceededIntegrationEvent = new ApplyWithdrawSucceededIntegrationEvent(@event.GamblerId, @event.Amount, @event.RequestId);
            await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(applyWithdrawSucceededIntegrationEvent);
            await _walletsIntegrationEventService.PublishThroughEventBusAsync(applyWithdrawSucceededIntegrationEvent);

            // publish WithdrawRequestSucceededIntegrationEvent (for Notifications)
            var withdrawRequestSucceededIntegrationEvent = new WithdrawRequestSucceededIntegrationEvent(@event.GamblerId, @event.RequestId, @event.Amount, wallet.Balance);
            await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(withdrawRequestSucceededIntegrationEvent);
            await _walletsIntegrationEventService.PublishThroughEventBusAsync(withdrawRequestSucceededIntegrationEvent);
        }
    }
}
