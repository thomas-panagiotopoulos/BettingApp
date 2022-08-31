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
    public class TopUpAcceptedByBankIntegrationEventHandler : IIntegrationEventHandler<TopUpAcceptedByBankIntegrationEvent>
    {
        private readonly ILogger<TopUpAcceptedByBankIntegrationEventHandler> _logger;
        private readonly IWalletsRepository _repository;
        private readonly IWalletsIntegrationEventService _walletsIntegrationEventService;

        public TopUpAcceptedByBankIntegrationEventHandler(ILogger<TopUpAcceptedByBankIntegrationEventHandler> logger,
                                                 IWalletsRepository repository,
                                                 IWalletsIntegrationEventService walletsIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _walletsIntegrationEventService = walletsIntegrationEventService;
        }

        public async Task Handle(TopUpAcceptedByBankIntegrationEvent @event)
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
                wallet.ApplyTopUpTransaction(@event.Amount, @event.RequestId);  
            }
            catch(Exception e)
            {
                _logger.LogInformation($"Exception: {e.Message}");

                // publish ApplyTopUpFailedIntegrationEvent (for Payment)
                var applyTopUpFailedIntegrationEvent = new ApplyTopUpFailedIntegrationEvent(@event.GamblerId, @event.Amount, @event.RequestId);
                await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(applyTopUpFailedIntegrationEvent);
                await _walletsIntegrationEventService.PublishThroughEventBusAsync(applyTopUpFailedIntegrationEvent);

                return;
            }

            // save changes
            await _repository.UnitOfWork.SaveChangesAsync();

            // publish UserWalletBalanceChangedIntegrationEvent (for Betslips)
            var walletBalanceChangedIntegrationEvent = new WalletBalanceChangedIntegrationEvent(wallet.GamblerId, wallet.Balance, wallet.PreviousBalance);
            await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(walletBalanceChangedIntegrationEvent);
            await _walletsIntegrationEventService.PublishThroughEventBusAsync(walletBalanceChangedIntegrationEvent);

            // publish ApplyTopUpSucceededIntegrationEvent (for Payment)
            var applyTopUpSucceededIntegrationEvent = new ApplyTopUpSucceededIntegrationEvent(@event.GamblerId, @event.Amount, @event.RequestId);
            await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(applyTopUpSucceededIntegrationEvent);
            await _walletsIntegrationEventService.PublishThroughEventBusAsync(applyTopUpSucceededIntegrationEvent);

            // publish TopUpRequestSucceededIntegrationEvent (for Notifications)
            var topUpRequestSucceededIntegrationEvent = new TopUpRequestSucceededIntegrationEvent(@event.GamblerId, @event.RequestId, @event.Amount, wallet.Balance);
            await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(topUpRequestSucceededIntegrationEvent);
            await _walletsIntegrationEventService.PublishThroughEventBusAsync(topUpRequestSucceededIntegrationEvent);
        }
    }
}
