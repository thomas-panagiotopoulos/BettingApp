using BettingApp.Services.Sportbook.API.DTOs;
using BettingApp.Services.Sportbook.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Extensions
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
                IsCanceled = match.IsCanceled,
                PossiblePickDTOs = match.PossiblePicks.ToPossiblePickDTOs().ToList(),
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
                MatchId = possiblePick.MatchId,
                MatchResultId = possiblePick.MatchResultId,
                MatchResultAliasName = MatchResult.From(possiblePick.MatchResultId).AliasName,
                MatchResultName = possiblePick.MatchResultName,
                Odd = possiblePick.Odd,
                InitialOdd = possiblePick.InitialOdd,
                RequirementTypeId = possiblePick.RequirementTypeId,
                RequirementTypeName = possiblePick.RequirementTypeName,
                RequirementTypeAliasName = RequirementType.From(possiblePick.RequirementTypeId).AliasName,
                RequiredValue = possiblePick.RequiredValue,
                IsBetable = possiblePick.IsBetable,
                IsCanceled = possiblePick.IsCanceled
            };
        }

        public static SelectionDTO ToSelectionDTO(this Match match, int matchResultId)
        {
            var possiblePick = match.PossiblePicks.SingleOrDefault(p => p.MatchResultId == matchResultId);

            return new SelectionDTO()
            {
                GamblerMatchResultId = possiblePick.MatchResultId,
                GamblerMatchResultName = possiblePick.MatchResultName,
                GamblerMatchResultAliasName = MatchResult.From(possiblePick.MatchResultId).AliasName,
                Odd = possiblePick.Odd,
                InitialOdd = possiblePick.InitialOdd,
                RelatedMatchId = match.Id,
                HomeClubName = match.HomeClubName,
                AwayClubName = match.AwayClubName,
                KickoffDateTime = match.KickoffDateTime,
                CurrentMinute = match.CurrentMinute,
                HomeClubScore = match.HomeClubScore,
                AwayClubScore = match.AwayClubScore,
                RequirementTypeId = possiblePick.RequirementTypeId,
                RequirementTypeName = possiblePick.RequirementTypeName,
                RequirementTypeAliasName = RequirementType.From(possiblePick.RequirementTypeId).AliasName,
                RequiredValue = possiblePick.RequiredValue,
                IsBetable = possiblePick.IsBetable,
            };
        }
    }
}
