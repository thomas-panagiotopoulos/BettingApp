using BettingApp.BuildingBlocks.EventBus.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.TestingService.API.IntegrationEvents.Events
{
    public class MatchCreatedIntegrationEvent : IntegrationEvent
    {
        public MatchDTO MatchDTO { get; private set; }

        public MatchCreatedIntegrationEvent(MatchDTO matchDto)
        {
            MatchDTO = matchDto;
        }
    }

    public class MatchDTO
    {
        public string MatchId { get; set; }

        public string HomeClubName { get; set; }

        public string AwayClubName { get; set; }

        public DateTime KickoffDateTime { get; set; }

        public string CurrentMinute { get; set; }

        public int HomeClubScore { get; set; }

        public int AwayClubScore { get; set; }

        public List<PossiblePickDTO> PossiblePickDTOs { get; set; }

        public MatchDTO(string matchId, string homeClubName, string awayClubName, DateTime kickoffDateTime,
                        string currentMinute, int homeClubScore, int awayClubScore, List<PossiblePickDTO> possiblePickDTOs)
        {
            MatchId = matchId;
            HomeClubName = homeClubName;
            AwayClubName = awayClubName;
            KickoffDateTime = kickoffDateTime;
            CurrentMinute = currentMinute;
            HomeClubScore = homeClubScore;
            AwayClubScore = awayClubScore;
            PossiblePickDTOs = possiblePickDTOs;
        }
    }

    public class PossiblePickDTO
    {
        public int MatchResultId { get; set; }
        public decimal Odd { get; set; }
        public int RequirementTypeId { get; set; }
        public decimal RequiredValue { get; set; }

        public PossiblePickDTO(int matchResultId, decimal odd, int requirementTypeId, decimal requiredValue)
        {
            MatchResultId = matchResultId;
            Odd = odd;
            RequirementTypeId = requirementTypeId;
            RequiredValue = requiredValue;
        }
    }
}
