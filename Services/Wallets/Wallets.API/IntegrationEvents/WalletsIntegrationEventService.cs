using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.BuildingBlocks.EventBus.Events;
using BettingApp.BuildingBlocks.IntegrationEventLogEF;
using BettingApp.BuildingBlocks.IntegrationEventLogEF.Services;
using BettingApp.BuildingBlocks.IntegrationEventLogEF.Utilities;
using BettingApp.Services.Wallets.API.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using BettingApp.Services.Wallets.API;

namespace BettingApp.Services.Wallets.API.IntegrationEvents
{
    public class WalletsIntegrationEventService : IWalletsIntegrationEventService
    {
        private readonly Func<DbConnection, IIntegrationEventLogService> _integrationEventLogServiceFactory;
        private readonly IEventBus _eventBus;
        private readonly WalletsContext _walletsContext;
        private readonly IIntegrationEventLogService _eventLogService;
        private readonly ILogger<WalletsIntegrationEventService> _logger;

        public WalletsIntegrationEventService(IEventBus eventBus,
            WalletsContext walletsContext,
            IntegrationEventLogContext eventLogContext,
            Func<DbConnection, IIntegrationEventLogService> integrationEventLogServiceFactory,
            ILogger<WalletsIntegrationEventService> logger)
        {
            _walletsContext = walletsContext ?? throw new ArgumentNullException(nameof(walletsContext));
            _integrationEventLogServiceFactory = integrationEventLogServiceFactory ?? throw new ArgumentNullException(nameof(integrationEventLogServiceFactory));
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _eventLogService = _integrationEventLogServiceFactory(_walletsContext.Database.GetDbConnection());
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            try
            {
                _logger.LogInformation("----- Publishing integration event: {IntegrationEventId_published} from {AppName} - ({@IntegrationEvent})", evt.Id, Program.AppName, evt);

                await _eventLogService.MarkEventAsInProgressAsync(evt.Id);
                _eventBus.Publish(evt);
                await _eventLogService.MarkEventAsPublishedAsync(evt.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", evt.Id, Program.AppName, evt);
                await _eventLogService.MarkEventAsFailedAsync(evt.Id);
            }
        }

        public async Task SaveEventAndWalletsContextChangesAsync(IntegrationEvent evt)
        {
            _logger.LogInformation("----- WalletsIntegrationEventService - Saving changes and integrationEvent: {IntegrationEventId}", evt.Id);

            //Use of an EF Core resiliency strategy when using multiple DbContexts within an explicit BeginTransaction():
            //See: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency            
            await ResilientTransaction.New(_walletsContext).ExecuteAsync(async () =>
            {
                // Achieving atomicity between original betslips database operation and the IntegrationEventLog thanks to a local transaction
                await _walletsContext.SaveChangesAsync();
                await _eventLogService.SaveEventAsync(evt, _walletsContext.Database.CurrentTransaction);
            });
        }
    }
}
