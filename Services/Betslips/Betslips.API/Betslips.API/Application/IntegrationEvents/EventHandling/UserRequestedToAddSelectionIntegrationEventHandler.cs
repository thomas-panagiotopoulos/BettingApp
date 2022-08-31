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
    public class UserRequestedToAddSelectionIntegrationEventHandler
        : IIntegrationEventHandler<UserRequestedToAddSelectionIntegrationEvent>
    {
        private readonly ILogger<UserRequestedToAddSelectionIntegrationEventHandler> _logger;
        private readonly IBetslipRepository _repository;
        private readonly IMediator _mediator;

        public UserRequestedToAddSelectionIntegrationEventHandler(ILogger<UserRequestedToAddSelectionIntegrationEventHandler> logger,
                                                                  IBetslipRepository repository,
                                                                  IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(UserRequestedToAddSelectionIntegrationEvent @event)
        {
            var exists = _repository.ExistsWithGamblerId(@event.GamblerId);

            if (!exists)
            {
                await _mediator.Send(new CreateBetslipCommand(@event.GamblerId));
            }

            var command = new AddSelectionCommand(@event.GamblerId, @event.SelectionDTO.GamblerMatchResultId,
                                                 @event.SelectionDTO.Odd, @event.SelectionDTO.InitialOdd, @event.SelectionDTO.RelatedMatchId,
                                                 @event.SelectionDTO.HomeClubName, @event.SelectionDTO.AwayClubName,
                                                 @event.SelectionDTO.KickoffDateTime, @event.SelectionDTO.CurrentMinute,
                                                 @event.SelectionDTO.HomeClubScore, @event.SelectionDTO.AwayClubScore,
                                                 @event.SelectionDTO.RequirementTypeId, @event.SelectionDTO.RequiredValue, @event.LatestAdditionId);

            await _mediator.Send(command);
        }
    }
}
