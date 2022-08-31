using BettingApp.Services.Betting.Domain.AggregatesModel.BetAggregate;
using BettingApp.Services.Betting.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betting.Infrastructure.Repositories
{
    public class BetRepository : IBetRepository
    {
        private readonly BettingContext _context;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }

        private int _pageSize = 10;

        public BetRepository(BettingContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Bet Add(Bet bet)
        {
            var entity = _context.Bets.Add(bet).Entity;

            return entity;
        }

        public void Update(Bet bet)
        {
            _context.Entry(bet).State = EntityState.Modified;
        }

        public bool RemoveById(string betId)
        {
            var bet = _context.Bets.FirstOrDefault(b => b.Id.Equals(betId));

            if (bet == null) return false;

            _context.Bets.Remove(bet);

            return true;
        }

        public async Task<Bet> GetAsync(string betId)
        {

            var bet = await _context.Bets
                              .Include(b => b.Status)
                              .Include(b => b.Result)
                              .Include(b => b.Selections).ThenInclude(s => s.Status)
                              .Include(b => b.Selections).ThenInclude(s => s.Result)
                              .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                              .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Status)
                              .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Result)
                              .Where(b => b.Id.Equals(betId))
                              .FirstOrDefaultAsync();

            // If bet is not found the DbSet, search in the LocalView for newly added bets.
            // Attention: cannot use async LINQ methods on Local as it doesn't support them
            if(bet == null)
            {

                bet =  _context.Bets
                              .Local 
                              .AsQueryable()
                              .Include(b => b.Status)
                              .Include(b => b.Result)
                              .Include(b => b.Selections).ThenInclude(s => s.Status)
                              .Include(b => b.Selections).ThenInclude(s => s.Result)
                              .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                              .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Status)
                              .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Result)
                              .Where(b => b.Id.Equals(betId))
                              .FirstOrDefault();
            }

            return bet;         
        }

        public async Task<Bet> GetByBetIdAndGamblerId(string gamblerId, string betId)
        {
            var bet = await _context.Bets
                                    .Where(b => b.Id.Equals(betId) && b.GamblerId.Equals(gamblerId))
                                    .Include(b => b.Status)
                                    .Include(b => b.Result)
                                    .Include(b => b.Selections).ThenInclude(s => s.Status)
                                    .Include(b => b.Selections).ThenInclude(s => s.Result)
                                    .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                                    .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Status)
                                    .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Result)
                                    .FirstOrDefaultAsync();

            // If bet is not found the DbSet, search in the LocalView for newly added bets.
            // Attention: cannot use async LINQ methods on Local as it doesn't support them
            if (bet == null)
            {
                bet = _context.Bets
                              .Local
                              .AsQueryable()
                              .Where(b => b.Id.Equals(betId) && b.GamblerId.Equals(gamblerId))
                              .Include(b => b.Status)
                              .Include(b => b.Result)
                              .Include(b => b.Selections).ThenInclude(s => s.Status)
                              .Include(b => b.Selections).ThenInclude(s => s.Result)
                              .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                              .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Status)
                              .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Result)
                              .FirstOrDefault();
            }

            return bet;
        }

        public bool Exists(string betId)
        {
            var exists =_context.Bets.Any(b => b.Id.Equals(betId));

            if (!exists)
            {
                exists = _context.Bets
                                .Local
                                .AsQueryable()
                                .Any(b => b.Id.Equals(betId));
            }

            return exists;
        }

        public async Task<IEnumerable<Bet>> GetAllAsync()
        {
            var bets = await _context.Bets
                              .Include(b => b.Status)
                              .Include(b => b.Result)
                              .Include(b => b.Selections).ThenInclude(s => s.Status)
                              .Include(b => b.Selections).ThenInclude(s => s.Result)
                              .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                              .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Status)
                              .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Result)
                              .ToListAsync();

            return bets;
        }

        public async Task<Bet> GetByMatchIdAsync(string matchId)
        {
            var bet = await _context.Bets
                                    .Where(b => b.Selections
                                                 .Where(s => s.Match.Id.Equals(matchId))
                                                 .Any())
                                    .Include(b => b.Status)
                                    .Include(b => b.Result)
                                    .Include(b => b.Selections).ThenInclude(s => s.Status)
                                    .Include(b => b.Selections).ThenInclude(s => s.Result)
                                    .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                                    .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Status)
                                    .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Result)
                                    .FirstOrDefaultAsync();

            // we can also look in the LocalView for newly created Bets, if Bet is not found in the DbContext

            return bet;
        }

        public async Task<Bet> GetBySelectionIdAsync(string selectionId)
        {
            var bet = await _context.Bets
                                    .Where(b => b.Selections
                                                 .Where(s => s.Id.Equals(selectionId))
                                                 .Any())
                                    .Include(b => b.Status)
                                    .Include(b => b.Result)
                                    .Include(b => b.Selections).ThenInclude(s => s.Status)
                                    .Include(b => b.Selections).ThenInclude(s => s.Result)
                                    .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                                    .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Status)
                                    .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Result)
                                    .FirstOrDefaultAsync();

            // we can also look in the LocalView for newly created Bets, if Bet is not found in the DbContext

            return bet;
        }

       public IEnumerable<Bet> GetBetsWithRelatedMatch(string relatedMatchId)
       {
            var bets = _context.Bets
                               .Where(b => b.Selections
                                            .Where(s => s.Match.RelatedMatchId.Equals(relatedMatchId))
                                            .Any())
                               .Include(b => b.Status)
                               .Include(b => b.Result)
                               .Include(b => b.Selections).ThenInclude(s => s.Status)
                               .Include(b => b.Selections).ThenInclude(s => s.Result)
                               .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                               .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Status)
                               .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Result)
                               .ToList();

            return bets;

        }

        public IEnumerable<Bet> GetBetsByGamblerId(string gamblerId)
        {
            var bets = _context.Bets
                               .Where(b => b.GamblerId.Equals(gamblerId))
                               .ToList();

            return bets;
        }

        public async Task<IEnumerable<Bet>> GetBetsPageByGamblerId(string gamblerId, int pageNumber)
        {
            // each page consists of 10 Bets always ordered by DateTimeCreated,
            // so for example PageNumber=1 will include the latest 10 Bets, PageNumber=2 will include
            // the latest 11 to 20 Bets, etc.
            if (pageNumber < 1)
                return null;

            var betsPage = await _context.Bets
                                         .Where(b => b.GamblerId.Equals(gamblerId))
                                         .OrderByDescending(b => b.DateTimeCreated)
                                         .Skip(pageNumber * _pageSize - _pageSize)
                                         .Take(_pageSize)
                                         .Include(b => b.Status)
                                         .Include(b => b.Result)
                                         .Include(b => b.Selections).ThenInclude(s => s.Status)
                                         .Include(b => b.Selections).ThenInclude(s => s.Result)
                                         .Include(b => b.Selections).ThenInclude(s => s.GamblerMatchResult)
                                         .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Status)
                                         .Include(b => b.Selections).ThenInclude(s => s.Match).ThenInclude(m => m.Result)
                                         .ToListAsync();
            return betsPage;
        }

        public int GetBetPreviewsPagesCount(string gamblerId)
        {
            var totalBets = _context.Bets.Where(b => b.GamblerId.Equals(gamblerId)).Count();

            return (totalBets / _pageSize) + (totalBets % _pageSize>0 ? 1:0);
        }
    }
}
