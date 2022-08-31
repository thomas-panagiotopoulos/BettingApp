using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.IntegrationEvents
{
    public interface IBetslipsIntegrationEventService
    {
        Task PublishEventsThroughEventBusAsync(Guid transactionId);
        Task AddAndSaveEventAsync(IntegrationEvent evt);

        Task PublishThroughEventBusAsync(IntegrationEvent evt);
        Task SaveEventAndBetslipsContextChangesAsync(IntegrationEvent evt);
    }
}
