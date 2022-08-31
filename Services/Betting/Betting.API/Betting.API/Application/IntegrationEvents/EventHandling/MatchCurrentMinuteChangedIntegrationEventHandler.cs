using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Betting.API.Application.Commands;
using BettingApp.Services.Betting.API.Application.IntegrationEvents.Events;
using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.IntegrationEvents.EventHandling
{
    public class MatchCurrentMinuteChangedIntegrationEventHandler
        : IIntegrationEventHandler<MatchCurrentMinuteChangedIntegrationEvent>
    {
        private readonly ILogger<MatchCurrentMinuteChangedIntegrationEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IMediator _mediator;

        public MatchCurrentMinuteChangedIntegrationEventHandler(
                                        ILogger<MatchCurrentMinuteChangedIntegrationEventHandler> logger,
                                        IBetRepository repository,
                                        IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(MatchCurrentMinuteChangedIntegrationEvent @event)
        {
            // Get all the Bets containing any Selection with the specific RelatedMatch
            var bets = _repository.GetBetsWithRelatedMatch(@event.MatchId);

            // Filter the valid Matches with the specific RelatedMatchId
            var matches = bets.Where(b => b.StatusId != Status.Completed.Id && b.StatusId != Status.Canceled.Id)
                              .SelectMany(b => b.Selections)
                              .Select(s => s.Match)
                              .Where(m => m.RelatedMatchId.Equals(@event.MatchId)
                                          && m.StatusId != Status.Canceled.Id
                                          && m.StatusId != Status.Completed.Id)
                              .ToList();

            // Send an UpdateMatchCurrentMinuteCommand for each of the Matches found
            foreach (var match in matches)
            {
                // We wrap every command in a try/catch block in order to prevent process stop execution if
                // a command throws an exception internally

                var command = new UpdateMatchCurrentMinuteCommand(match.Id, @event.NewCurrentMinute);
                try
                {
                    await _mediator.Send(command);
                }
                catch(Exception ex)
                {
                    _logger.LogError($"ERROR sending UpdateMatchCurrentMinuteCommand for MatchId '{command.MatchId}': "+ex.Message);
                }
                
            }

        }
    }
}
