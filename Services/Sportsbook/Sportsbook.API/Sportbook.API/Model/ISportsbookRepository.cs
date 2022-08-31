using BettingApp.Services.Sportbook.API.Model.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Model
{
    public interface ISportsbookRepository : IRepository
    {
        Match AddMatch(Match match);

        Task<List<Match>> GetAllMatchesAsync();

        Task<List<Match>> GetMatchesByDate(DateTime date);

        Task<Match> GetMatchAsync(string matchId);

        bool MatchExists(string matchId);
    }
}
