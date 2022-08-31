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
    public class MatchScoresChangedIntegrationEventHandler : IIntegrationEventHandler<MatchScoresChangedIntegrationEvent>
    {
        private readonly ILogger<MatchScoresChangedIntegrationEventHandler> _logger;
        private readonly IBetslipRepository _repository;
        private readonly IMediator _mediator;

        public MatchScoresChangedIntegrationEventHandler(ILogger<MatchScoresChangedIntegrationEventHandler> logger,
                                                         IBetslipRepository repository,
                                                         IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(MatchScoresChangedIntegrationEvent @event)
        {
            // Get all the Betslips containing any Selection with the specific RelatedMatch
            var betslips = _repository.GetBetslipsWithRelatedMatch(@event.MatchId);

            // Filter the Matches with the specific RelatedMatchId
            var matches = betslips.SelectMany(b => b.Selections)
                              .Select(s => s.Match)
                              .Where(m => m.RelatedMatchId.Equals(@event.MatchId) && !m.IsCanceled)
                              .ToList();

            // Send an UpdateMatchScoresCommand for each of the Matches found
            foreach (var match in matches)
            {
                // We wrap every command in a try/catch block in order to prevent process stop execution if
                // a command throws an exception internally

                var command = new UpdateMatchScoresCommand(match.Id, @event.NewHomeClubScore, @event.NewAwayClubScore);
                try
                {
                    await _mediator.Send(command);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ERROR sending UpdateMatchScoresCommand for MatchId '{command.MatchId}': " + ex.Message);
                }

            }
        }
    }
}
