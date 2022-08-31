using BettingApp.Services.Betting.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate
{
    public interface IBetRepository : IRepository<Bet>
    {
        Bet Add(Bet bet);

        void Update(Bet bet);

        bool RemoveById(string betId);

        Task<Bet> GetAsync(string betId);

        Task<Bet> GetByBetIdAndGamblerId(string gamblerId, string betId);

        bool Exists(string betId);

        Task<IEnumerable<Bet>> GetAllAsync();

        Task<Bet> GetByMatchIdAsync(string matchId);

        Task<Bet> GetBySelectionIdAsync(string selectionId);

        IEnumerable<Bet> GetBetsWithRelatedMatch(string relatedMatchId);

        IEnumerable<Bet> GetBetsByGamblerId(string gamblerId);

        Task<IEnumerable<Bet>> GetBetsPageByGamblerId(string gamblerId, int pageNumber);

        int GetBetPreviewsPagesCount(string gamblerId);
    }
}
