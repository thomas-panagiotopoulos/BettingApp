using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.TestingService.API.IntegrationEvents
{
    public interface ITestingIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
        Task SaveEventAndTestingContextChangesAsync(IntegrationEvent evt);
    }
}
