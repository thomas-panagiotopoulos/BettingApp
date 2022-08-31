using BettingApp.Services.Betslips.API.Application.DTOs;
using BettingApp.Services.Betslips.Domain.AggregatesModel.BetslipAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.API.Extensions
{
    public static class BetslipExtensionMethods
    {
        public static BetslipDTO ToBetslipDTO(this Betslip betslip)
        {
            return new BetslipDTO()
            {
                GamblerId = betslip.GamblerId,
                WageredAmount = betslip.WageredAmount,
                SelectionDTOs = betslip.Selections.ToSelectionDTOs().ToList(),
                TotalOdd = betslip.TotalOdd,
                PotentialWinnings = betslip.PotentialWinnings,
                PotentialProfit = betslip.PotentialProfit,
                IsBetable = betslip.IsBetable,
                MaxSelectionsLimit = betslip.MaxSelectionsLimit,
            };
        }

        public static IEnumerable<SelectionDTO> ToSelectionDTOs(this IEnumerable<Selection> selections)
        {
            foreach(var selection in selections)
            {
                yield return selection.ToSelectionDTO();
            }
        }

        public static SelectionDTO ToSelectionDTO(this Selection selection)
        {
            return new SelectionDTO()
            {
                Id = selection.Id,
                GamblerMatchResultId = selection.GamblerMatchResultId,
                GamblerMatchResultName = selection.GamblerMatchResultName,
                GamblerMatchResultAliasName = MatchResult.From(selection.GamblerMatchResultId).AliasName,
                SelectionTypeName = selection.SelectionTypeName,
                Odd = selection.Odd,
                InitialOdd = selection.InitialOdd,
                RelatedMatchId = selection.Match.RelatedMatchId,
                HomeClubName = selection.Match.HomeClubName,
                AwayClubName = selection.Match.AwayClubName,
                KickoffDateTime = selection.Match.KickoffDateTime,
                CurrentMinute = selection.Match.CurrentMinute,
                HomeClubScore = selection.Match.HomeClubScore,
                AwayClubScore = selection.Match.AwayClubScore,
                RequirementTypeId = selection.Requirement.RequirementTypeId,
                RequirementTypeName = selection.Requirement.RequirementTypeName,
                RequirementTypeAliasName = RequirementType.From(selection.Requirement.RequirementTypeId).AliasName,
                RequiredValue = selection.Requirement.RequiredValue,
                IsBetable = selection.IsBetable,
                IsCanceled = selection.IsCanceled
            };
        }
    }
}
