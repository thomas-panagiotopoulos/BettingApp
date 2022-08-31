using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Sportbook.API.IntegrationEvents.Events;
using BettingApp.Services.Sportbook.API.Model;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.IntegrationEvents.EventHandling
{
    public class MatchResultDisabledIntegrationEventHandler : IIntegrationEventHandler<MatchResultDisabledIntegrationEvent>
    {
        private readonly ILogger<MatchResultDisabledIntegrationEventHandler> _logger;
        private readonly ISportsbookRepository _repository;

        public MatchResultDisabledIntegrationEventHandler(ILogger<MatchResultDisabledIntegrationEventHandler> logger,
                                                          ISportsbookRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(MatchResultDisabledIntegrationEvent @event)
        {
            // Get the Match from DB using the repository
            var match = await _repository.GetMatchAsync(@event.MatchId);

            // Update Match (we use null-conditional operator to ensure that
            // no NullReferenceException will be thrown in case Match is not found and thus is null)
            match?.DisablePossiblePick(@event.MatchResultId);

            // Save changes to the DB
            await _repository.UnitOfWork.SaveChangesAsync();
        }
    }
}
