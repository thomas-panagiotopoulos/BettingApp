using BettingApp.BuildingBlocks.EventBus.Extensions;
using BettingApp.Services.Betslips.API.Application.IntegrationEvents;
using BettingApp.Services.Betslips.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.Behaviors
{
    public class TransactionBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TransactionBehavior<TRequest, TResponse>> _logger;
        private readonly IBetslipsIntegrationEventService _betslipsIntegrationEventService;
        private readonly BetslipsContext _dbContext;

        public TransactionBehavior(ILogger<TransactionBehavior<TRequest, TResponse>> logger,
                                   IBetslipsIntegrationEventService betslipsIntegrationEventService,
                                   BetslipsContext dbContext)
        {
            _logger = logger;
            _betslipsIntegrationEventService = betslipsIntegrationEventService;
            _dbContext = dbContext;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var response = default(TResponse);
            var typeName = request.GetGenericTypeName();

            try
            {
                if (_dbContext.HasActiveTransaction)
                {
                    return await next();
                }

                var strategy = _dbContext.Database.CreateExecutionStrategy();

                await strategy.ExecuteAsync(async () =>
                {
                    Guid transactionId;

                    using (var transaction = await _dbContext.BeginTransactionAsync())
                    {
                        _logger.LogInformation("----- Begin transaction {TransactionId} for {CommandName} ({@Command})", transaction.TransactionId, typeName, request);
                        
                        response = await next();

                        _logger.LogInformation("----- Commit transaction {TransactionId} for {CommandName}", transaction.TransactionId, typeName);

                        await _dbContext.CommitTransactionAsync(transaction);

                        transactionId = transaction.TransactionId;
                    }

                    // Publish all the Integration events produced in the lifetime of this transaction
                    await _betslipsIntegrationEventService.PublishEventsThroughEventBusAsync(transactionId);
                    
                    _logger.LogInformation($"All Integration events produced during the lifetime of transaction with Id:{transactionId}" +
                                            " were succefully published through the Event Bus.");
                });

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Handling transaction for {CommandName} ({@Command})", typeName, request);
                _logger.LogError($"Rolling back transaction with Id: '{_dbContext.GetCurrentTransaction().TransactionId}'.");
                _dbContext.RollbackTransaction();
                throw;
            }
        }
    }
}
