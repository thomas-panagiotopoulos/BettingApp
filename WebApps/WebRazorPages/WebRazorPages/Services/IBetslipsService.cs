using BettingApp.WebApps.WebRazorPages.Models;
using BettingApp.WebApps.WebRazorPages.Services.ModelDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Services
{
    public interface IBetslipsService
    {
        Task<Betslip> GetBetslip();
        Task<bool> CheckAddSelection(string matchId, int matchResultId);
        Task<bool> VerifyLatestAddition(string latestAdditionId);
        Task<Betslip> AddSelection(SelectionDTO selection);
        Task<bool> RemoveSelection(string selectionId);
        Task<bool> UpdateWageredAmount(decimal wageredAmount);
        Task<bool> ClearBetslip();
        Task<string> Checkout();
        Task<decimal> GetWalletBalance();
    }
}
