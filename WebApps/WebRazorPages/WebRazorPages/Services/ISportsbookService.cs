using BettingApp.WebApps.WebRazorPages.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Services
{
    public interface ISportsbookService
    {
        Task<List<Match>> GetMatchesByDate(DateTime date);

        Task<List<League>> GetLeaguesByDate(DateTime date);

        Task<List<MatchResult>> GetMatchResults();

        Task<Match> GetMatch(string matchId);

        Task<Selection> GetSelection(string matchId, int matchResultId);

        Task<string> RequestToAddSelection(string matchId, int gamblerMatchResultId);
    }
}
