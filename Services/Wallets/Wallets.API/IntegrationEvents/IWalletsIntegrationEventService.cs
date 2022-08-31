using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.IntegrationEvents
{
    public interface IWalletsIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
        Task SaveEventAndWalletsContextChangesAsync(IntegrationEvent evt);
    }
}
