using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate;
using BettingApp.Services.MatchSimulation.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Infrastructure.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly MatchSimulationContext _context;

        public MatchRepository(MatchSimulationContext context)
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

        public Match Add(Match match)
        {
            var entity = _context.Matches.Add(match).Entity;

            return entity;
        }

        public async Task<Match> GetByIdAsync(string matchId)
        {
            var match = await _context.Matches
                                     .Include(m => m.League)
                                     .Include(m => m.PossiblePicks).ThenInclude(p => p.MatchResult)
                                     .Include(m => m.PossiblePicks).ThenInclude(p => p.RequirementType)
                                     .SingleOrDefaultAsync(m => m.Id.Equals(matchId));

            // If Match was not found, look for it in the LocalView
            // (newly created Matches that are not persisted are located there)
            if (match == null)
            {
                match = _context.Matches
                                .Local
                                .AsQueryable()
                                .Include(m => m.League)
                                .Include(m => m.PossiblePicks).ThenInclude(p => p.MatchResult)
                                .Include(m => m.PossiblePicks).ThenInclude(p => p.RequirementType)
                                .SingleOrDefault(m => m.Id.Equals(matchId));
            }
            
            return match;
        }

        public async Task<Match> GetBySimulationIdAsync(string simulationId)
        {
            var match = await _context.Matches
                                     .Include(m => m.League)
                                     .Include(m => m.PossiblePicks).ThenInclude(p => p.MatchResult)
                                     .Include(m => m.PossiblePicks).ThenInclude(p => p.RequirementType)
                                     .SingleOrDefaultAsync(m => m.SimulationId.Equals(simulationId));

            if (match == null)
            {
                match = _context.Matches
                                .Local
                                .AsQueryable()
                                .Include(m => m.League)
                                .Include(m => m.PossiblePicks).ThenInclude(p => p.MatchResult)
                                .Include(m => m.PossiblePicks).ThenInclude(p => p.RequirementType)
                                .SingleOrDefault(m => m.SimulationId.Equals(simulationId));
            }

            return match;
        }

        public async Task<List<Match>> GetMatchesByKickoffDate(DateTime date)
        {
            var matches = await _context.Matches
                                        .Where(m => DateTime.Compare(m.KickoffDateTime.Date, date.Date) == 0)
                                        .ToListAsync();

            return matches;
        }

        public async Task<bool> ExistsWithKickoffDate(DateTime date)
        {
            return await _context.Matches.AnyAsync(m => DateTime.Compare(m.KickoffDateTime.Date, date.Date) == 0);
        }

        public async Task<bool> ExistsWithIdAsync(string matchId)
        {
            return await _context.Matches.AnyAsync(m => m.Id.Equals(matchId));
        }
    }
}
