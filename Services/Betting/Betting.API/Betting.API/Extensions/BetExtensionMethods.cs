using BettingApp.Services.Betting.API.Application.DTOs;
using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.API.Extensions
{
    public static class BetExtensionMethods
    {
        public static BetDTO ToBetDTO(this Bet bet)
        {
            return new BetDTO()
            {
                Id = bet.Id,
                GamblerId = bet.GamblerId,
                DateTimeCreated = bet.DateTimeCreated,
                IsPaid = bet.IsPaid,
                IsCancelable = bet.IsCancelable,
                StatusId = bet.StatusId,
                StatusName = bet.StatusName,
                ResultId = bet.ResultId,
                ResultName = bet.ResultName,
                WageredAmount = bet.WageredAmount,
                TotalOdd = bet.TotalOdd,
                PotentialWinnings = bet.PotentialWinnings,
                PotentialProfit = bet.PotentialProfit,
                InitialTotalOdd = bet.InitialTotalOdd,
                InitialPotentialWinnings = bet.InitialPotentialWinnings,
                InitialPotentialProfit = bet.InitialPotentialProfit,
                SelectionDTOs = bet.Selections.ToSelectionDTOs().ToList()
            };
        }

        public static BetPreviewDTO ToBetPreviewDTO(this Bet bet)
        {
            return new BetPreviewDTO()
            {
                Id = bet.Id,
                GamblerId = bet.GamblerId,
                DateTimeCreated = bet.DateTimeCreated,
                IsPaid = bet.IsPaid,
                IsCancelable = bet.IsCancelable,
                StatusId = bet.StatusId,
                StatusName = bet.StatusName,
                ResultId = bet.ResultId,
                ResultName = bet.ResultName,
                WageredAmount = bet.WageredAmount,
                TotalOdd = bet.TotalOdd,
                PotentialWinnings = bet.PotentialWinnings,
                PotentialProfit = bet.PotentialProfit,
                InitialTotalOdd = bet.InitialTotalOdd,
                InitialPotentialWinnings = bet.InitialPotentialWinnings,
                InitialPotentialProfit = bet.InitialPotentialProfit,
                SelectionsCount = bet.Selections.Count(),
            };
        }

        public static IEnumerable<SelectionDTO> ToSelectionDTOs(this IEnumerable<Selection> selections)
        {
            foreach (var selection in selections)
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
                RelatedMatchId = selection.Match.RelatedMatchId,
                HomeClubName = selection.Match.HomeClubName,
                AwayClubName = selection.Match.AwayClubName,
                KickoffDateTime = selection.Match.KickoffDateTime,
                CurrentMinute = selection.Match.CurrentMinute,
                HomeClubScore = selection.Match.HomeClubScore,
                AwayClubScore = selection.Match.AwayClubScore,
                StatusId = selection.StatusId,
                StatusName = selection.StatusName,
                IsCanceled = selection.StatusId == Status.Canceled.Id,
                ResultId = selection.ResultId,
                ResultName = selection.ResultName
            };
        }
    }
}
