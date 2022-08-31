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
    public class BetCompletedAsWonIntegrationEventHandler : IIntegrationEventHandler<BetCompletedAsWonIntegrationEvent>
    {
        private readonly ILogger<BetCompletedAsWonIntegrationEventHandler> _logger;
        private readonly IWalletsRepository _repository;
        private readonly IWalletsIntegrationEventService _walletsIntegrationEventService;

        public BetCompletedAsWonIntegrationEventHandler(ILogger<BetCompletedAsWonIntegrationEventHandler> logger,
                                                        IWalletsRepository repository,
                                                        IWalletsIntegrationEventService walletsIntegrationEventService)
        {
            _logger = logger;
            _repository = repository;
            _walletsIntegrationEventService = walletsIntegrationEventService;
        }

        public async Task Handle(BetCompletedAsWonIntegrationEvent @event)
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
            wallet.ApplyBetWinningsTransaction(@event.TotalWinnings, @event.BetId);

            // save changes
            await _repository.UnitOfWork.SaveChangesAsync();

            // publish UserWalletBalanceChangedIntegrationEvent (for Betslips)
            var walletBalanceChangedIntegrationEvent = new WalletBalanceChangedIntegrationEvent(wallet.GamblerId, wallet.Balance, wallet.PreviousBalance);
            await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(walletBalanceChangedIntegrationEvent);
            await _walletsIntegrationEventService.PublishThroughEventBusAsync(walletBalanceChangedIntegrationEvent);

            // publish BetWinningsCreditedIntegrationEvent (for Notifications)
            var betWinningsCreditedIntegrationEvent = new BetWinningsCreditedIntegrationEvent(@event.GamblerId, @event.BetId, @event.TotalWinnings, wallet.Balance);
            await _walletsIntegrationEventService.SaveEventAndWalletsContextChangesAsync(betWinningsCreditedIntegrationEvent);
            await _walletsIntegrationEventService.PublishThroughEventBusAsync(betWinningsCreditedIntegrationEvent);
        }
    }
}
