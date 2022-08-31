using BettingApp.Services.Sportbook.API.Model;
using BettingApp.Services.Sportbook.API.Model.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.Infrastructure.Repositories
{
    public class SportsbookRepository : ISportsbookRepository
    {
        private readonly SportsbookContext _context;

        public SportsbookRepository(SportsbookContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }


        public  Match AddMatch(Match match)
        {
            var entity = _context.Matches.Add(match).Entity;
            
            return entity;
        }

        public async Task<List<Match>> GetAllMatchesAsync()
        {
            var matchesList = await _context.Matches
                                      .Include(m => m.PossiblePicks).ThenInclude(p => p.MatchResult)
                                      .Include(m => m.PossiblePicks).ThenInclude(p => p.RequirementType)
                                      .OrderBy(m => m.KickoffDateTime)
                                      .ToListAsync();
                                      
            return matchesList;
        }

        public async Task<List<Match>> GetMatchesByDate(DateTime date)
        {
            var matchesList = await _context.Matches
                                            .Where(m => m.KickoffDateTime.Date.CompareTo(date.Date) == 0)
                                            .Include(m => m.PossiblePicks).ThenInclude(p => p.MatchResult)
                                            .Include(m => m.PossiblePicks).ThenInclude(p => p.RequirementType)
                                            .OrderBy(m => m.KickoffDateTime)
                                            .ToListAsync();
            return matchesList;
        }

        public async Task<Match> GetMatchAsync(string matchId)
        {
            var match = await _context.Matches
                                .Include(m => m.PossiblePicks).ThenInclude(p => p.MatchResult)
                                .Include(m => m.PossiblePicks).ThenInclude(p => p.RequirementType)
                                .FirstOrDefaultAsync(m => m.Id.Equals(matchId));

            // If match is not found in the DbSet, search in the LocalView for newly added matches.
            // Attention: cannot use async LINQ methods on Local as it doesn't support them
            // Note: not really necessary in this project as there are no cases were we retireve
            // a newly created match from the repository before calling SaveChanges()
            if (match == null)
            {
                match = _context.Matches
                                .Local
                                .AsQueryable()
                                .Include(m => m.PossiblePicks).ThenInclude(p => p.MatchResult)
                                .Include(m => m.PossiblePicks).ThenInclude(p => p.RequirementType)
                                .FirstOrDefault(m => m.Id.Equals(matchId));
            }

            return match;
        }

        public bool MatchExists(string matchId)
        {
            return _context.Matches.Any(m => m.Id.Equals(matchId));
        }
    }
}
