using BettingApp.BuildingBlocks.EventBus.Abstractions;
using BettingApp.Services.Betting.API.Application.Commands;
using BettingApp.Services.Betting.API.Application.IntegrationEvents.Events;
using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.IntegrationEvents.EventHandling
{
    public class UserCheckedOutIntegrationEventHandler : IIntegrationEventHandler<UserCheckedOutIntegrationEvent>
    {
        private readonly ILogger<UserCheckedOutIntegrationEventHandler> _logger;
        private readonly IBetRepository _repository;
        private readonly IMediator _mediator;

        public UserCheckedOutIntegrationEventHandler(ILogger<UserCheckedOutIntegrationEventHandler> logger,
                                                    IBetRepository repository,
                                                    IMediator mediator)
        {
            _logger = logger;
            _repository = repository;
            _mediator = mediator;
        }

        public async Task Handle(UserCheckedOutIntegrationEvent @event)
        {
            // First, we create a human-readable unique Id for the Bet that will be created
            var betId = CreateUniqueReadableId(12);

            // Next, we send an embedded CreateBetCommand, inside of an IdentifiedCommand,
            // to ensure no duplicate Bets will be created
            var embeddedCommand = new CreateBetCommand(betId, @event.BetslipDTO.GamblerId, 
                                                        @event.BetslipDTO.WageredAmount, 
                                                        @event.BetslipDTO.SelectionDTOs);

            //var command = new IdentifiedCommand<CreateBetCommand, Bet>(embeddedCommand, @event.RequestId, userId: @event.BetslipDTO.GamblerId);
            var command = new IdentifiedCommand<CreateBetCommand, Bet>(embeddedCommand, @event.RequestId);
            var result = await _mediator.Send(command);
        }

        // helper method that creates a Human-readable Id of given length, guarantying its uniquness
        // by querying the Repository before returning a value.
        // If a unique Id is not found after 100 tries, then it returns null
        private string CreateUniqueReadableId(int length)
        {
            // initialize the characters to be used for Id, a Random number generator and a StringBuilder
            char[] _base62chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".ToCharArray();
            Random _random = new Random();
            var sb = new StringBuilder(length);

            // create a random Id (use only base36 chars)
            for (int i = 0; i < length; i++)
                sb.Append(_base62chars[_random.Next(36)]);

            // if Id already exists in DB, then create a new one and check again,
            // until a unique Id is created or max tries (100) are reached
            var tries = 0;
            while (_repository.Exists(sb.ToString()) && tries < 100)
            {
                sb.Clear();
                tries++;
                for (int i = 0; i < length; i++)
                    sb.Append(_base62chars[_random.Next(36)]);
            }

            // if total tries is at max value (100) , this means no unique Id was created, so we return null
            if (tries >= 100) return null;

            // if we got here, the created Id is unique and we can return it safely
            return sb.ToString();
        }
    }
}
