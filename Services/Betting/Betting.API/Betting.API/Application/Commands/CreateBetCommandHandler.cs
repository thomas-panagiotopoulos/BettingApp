using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Application.Commands
{
    public class CreateBetCommandHandler : IRequestHandler<CreateBetCommand, Bet>
    {
        private readonly ILogger<CreateBetCommandHandler> _logger;
        private readonly IBetRepository _repository;

        public CreateBetCommandHandler(ILogger<CreateBetCommandHandler> logger, IBetRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<Bet> Handle(CreateBetCommand request, CancellationToken cancellationToken)
        {

            // Create the new Bet (this constructor internally adds a BetStartedDomainEvent to the queue)
            var createdBet = new Bet(request.BetId, request.GamblerId, request.WageredAmount);

            // Add the Selections to the newly created Bet
            foreach (var selectionDto in request.SelectionDTOs)
            {
                createdBet.AddSelection(selectionDto.GamblerMatchResultId, selectionDto.Odd, selectionDto.RelatedMatchId,
                                        selectionDto.HomeClubName, selectionDto.AwayClubName, selectionDto.KickoffDateTime,
                                        selectionDto.CurrentMinute, selectionDto.HomeClubScore, selectionDto.AwayClubScore);
            }

            // Add the new Bet to Bets table of the DB using the repository
            _repository.Add(createdBet);

            // SaveEntitiesAsync (dispatch Domain events and then SaveChanges)
            await _repository.UnitOfWork
                .SaveEntitiesAsync(cancellationToken);

            return createdBet;
        }
    }
}
