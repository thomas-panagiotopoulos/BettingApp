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
    public class MatchCreatedIntegrationEventHandler : IIntegrationEventHandler<MatchCreatedIntegrationEvent>
    {
        private readonly ILogger<MatchCreatedIntegrationEventHandler> _logger;
        private readonly ISportsbookRepository _repository;

        public MatchCreatedIntegrationEventHandler(ILogger<MatchCreatedIntegrationEventHandler> logger,
                                                      ISportsbookRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task Handle(MatchCreatedIntegrationEvent @event)
        {
            // Check if a match with the same Id exists already in the DB
            var exists = _repository.MatchExists(@event.MatchDTO.MatchId);

            if (exists)
            {
                _logger.LogInformation($"A Match with Id:{@event.MatchDTO.MatchId} already exists in the DB.");
                return;
            }

            // First create a list containing the PossiblePicks for the Match
            var possiblePicksList = new List<PossiblePick>();

            foreach (var matchResult in MatchResult.List())
            {
                var possiblePick = @event.MatchDTO.PossiblePickDTOs.SingleOrDefault(p => p.MatchResultId == matchResult.Id);

                if (possiblePick == null) continue;

                possiblePicksList.Add(new PossiblePick(@event.MatchDTO.MatchId, possiblePick.MatchResultId, 
                                                        possiblePick.Odd, possiblePick.RequirementTypeId, 
                                                        possiblePick.RequiredValue));

            }

            //Then create the Match
            var match = new Match(@event.MatchDTO.MatchId, @event.MatchDTO.HomeClubName, @event.MatchDTO.AwayClubName, 
                                @event.MatchDTO.LeagueId, @event.MatchDTO.KickoffDateTime, @event.MatchDTO.CurrentMinute, 
                                @event.MatchDTO.HomeClubScore, @event.MatchDTO.AwayClubScore, possiblePicksList);

            // Add the newly created Match to the Matches table of the DB through the repository
            _repository.AddMatch(match);

            // Save changes to the DB
            await _repository.UnitOfWork.SaveChangesAsync();

        }
    }
}
