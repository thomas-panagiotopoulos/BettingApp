using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.IntegrationEvents
{
    public interface ISportsbookIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
        Task SaveEventAndSportsbookContextChangesAsync(IntegrationEvent evt);
    }
}
