using BettingApp.WebApps.WebRazorPages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Services
{
    public interface IBettingService
    {
        Task<Bet> GetBet(string betId);
        Task<IEnumerable<BetPreview>> GetBetPreviewsPage(int pageNumber);
        Task<int> GetBetPreviewsPagesCount();
        Task<bool> CancelBet(string betId);
        Task<bool> RequestExists(string requestId);
    }
}
