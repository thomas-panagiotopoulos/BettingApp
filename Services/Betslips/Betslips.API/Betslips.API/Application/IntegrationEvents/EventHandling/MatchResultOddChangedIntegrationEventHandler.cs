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
    public class MatchResultOddChangedIntegrationEventHandler : IIntegrationEventHandler<MatchResultOddChangedIntegrationEvent>
    {
        private readonly ILogger<MatchResultOddChangedIntegrationEventHandler> _logger;
        private readonly IBetslipRepository _repository;
        private readonly IMediator _mediator;

        public MatchResultOddChangedIntegrationEventHandler(ILogger<MatchResultOddChangedIntegrationEventHandler> logger,
                                                          IBetslipRepository repository,
                                                          IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(MatchResultOddChangedIntegrationEvent @event)
        {
            // Get all the Betslips containing any Selection with the specific RelatedMatch
            var betslips = _repository.GetBetslipsWithRelatedMatch(@event.MatchId);

            // Filter the Selections with the specific Related Match and GamblerMatchResultId
            var selections = betslips.SelectMany(b => b.Selections)
                                  .Where(s => s.Match.RelatedMatchId.Equals(@event.MatchId) 
                                              && s.GamblerMatchResultId == @event.MatchResultId 
                                              && !s.IsCanceled)
                                  .ToList();

            // Send an UpdateSelectionOddCommand for each of the Selections found
            foreach (var selection in selections)
            {
                var command = new UpdateSelectionOddCommand(selection.Id, @event.NewOdd);
                try
                {
                    await _mediator.Send(command);
                }
                catch (Exception ex)
                {
                    _logger.LogError($"ERROR sending UpdateSelectionOddCommand for SelectionId '{command.SelectionId}': " + ex.Message);
                }
            }
        }
    }
}
