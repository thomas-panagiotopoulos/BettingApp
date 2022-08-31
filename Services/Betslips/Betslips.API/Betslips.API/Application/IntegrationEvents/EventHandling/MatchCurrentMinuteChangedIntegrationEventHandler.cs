using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Betslips.API.Application.Commands;
using BettingApp.Services.Betslips.API.Application.IntegrationEvents.Events;
using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Application.IntegrationEvents.EventHandling
{
    public class MatchCurrentMinuteChangedIntegrationEventHandler : IIntegrationEventHandler<MatchCurrentMinuteChangedIntegrationEvent>
    {
        private readonly ILogger<MatchCurrentMinuteChangedIntegrationEventHandler> _logger;
        private readonly IMediator _mediator;
        private readonly IBetslipRepository _repository;

        public MatchCurrentMinuteChangedIntegrationEventHandler(ILogger<MatchCurrentMinuteChangedIntegrationEventHandler> logger,
                                                                IMediator mediator,
                                                                IBetslipRepository repository)
        {
            _logger = logger;
            _mediator = mediator;
            _repository = repository;
        }

        public async Task Handle(MatchCurrentMinuteChangedIntegrationEvent @event)
        {
            // Get all the Betslips containing any Selection with the specific RelatedMatch
            var betslips = _repository.GetBetslipsWithRelatedMatch(@event.MatchId);

            // Filter the Matches with the specific RelatedMatchId
            var matches = betslips.SelectMany(b => b.Selections)
                                  .Select(s => s.Match)
                                  .Where(m => m.RelatedMatchId.Equals(@event.MatchId) && !m.IsCanceled)
                                  .ToList();

            // Send an UpdateMatchCurrentMinuteCommand for each of the Matches found
            foreach (var match in matches)
            {
                var command = new UpdateMatchCurrentMinuteCommand(match.Id, @event.NewCurrentMinute);
                try
                {
                    await _mediator.Send(command);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ERROR sending UpdateMatchCurrentMinuteCommand for MatchId '{command.MatchId}': " + ex.Message);
                }
            }
                                
        }
    }
}
