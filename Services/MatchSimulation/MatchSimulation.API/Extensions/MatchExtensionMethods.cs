using BettingApp.Services.MatchSimulation.API.Application.DTOs;
using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Extensions
{
    public static class MatchExtensionMethods
    {
        public static MatchDTO ToMatchDTO(this Match match)
        {
            return new MatchDTO()
            {
                MatchId = match.Id,
                HomeClubName = match.HomeClubName,
                AwayClubName = match.AwayClubName,
                LeagueId = match.LeagueId,
                LeagueName = match.LeagueName,
                KickoffDateTime = match.KickoffDateTime,
                CurrentMinute = match.CurrentMinute,
                HomeClubScore = match.HomeClubScore,
                AwayClubScore = match.AwayClubScore,
                PossiblePickDTOs = match.PossiblePicks.ToPossiblePickDTOs().ToList()
            };
        }

        public static IEnumerable<PossiblePickDTO> ToPossiblePickDTOs(this IEnumerable<PossiblePick> possiblePicks)
        {
            foreach (var possiblePick in possiblePicks)
            {
                yield return possiblePick.ToPossiblePickDTO();
            }
        }

        public static PossiblePickDTO ToPossiblePickDTO(this PossiblePick possiblePick)
        {
            return new PossiblePickDTO()
            {
                MatchResultId = possiblePick.MatchResultId,
                Odd = possiblePick.Odd,
                RequirementTypeId = possiblePick.RequirementTypeId,
                RequiredValue = possiblePick.RequiredValue
            };
        }
    }
}
