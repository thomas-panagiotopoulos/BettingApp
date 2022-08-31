using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate;
using BettingApp.Services.MatchSimulation.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Infrastructure.Repositories
{
    public class ClubRepository : IClubRepository
    {
        private readonly MatchSimulationContext _context;

        public ClubRepository(MatchSimulationContext context)
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

        public async Task<Club> GetByIdAsync(string clubId)
        {
            var club = await _context.Clubs
                                     //.Include(c => c.DomesticLeague)
                                     //.Include(c => c.ContinentalLeague)
                                     .SingleOrDefaultAsync(c => c.Id.Equals(clubId));
            return club;
        }

        public async Task<List<Club>> GetClubsByActiveSimulationIdAsync(string simulationId)
        {
            var clubs = await _context.Clubs
                                      .Where(c => c.HasActiveSimulation && c.ActiveSimulationId.Equals(simulationId))
                                      //.Include(c => c.DomesticLeague)
                                      //.Include(c => c.ContinentalLeague)
                                      .ToListAsync();

            return clubs;
        }

        public async Task<List<Club>> GetClubsByDomesticLeagueId(int domesticLeagueId)
        {
            var clubs = await _context.Clubs
                                      .Where(c => c.DomesticLeagueId == domesticLeagueId)
                                      //.Include(c => c.DomesticLeague)
                                      //.Include(c => c.ContinentalLeague)
                                      .ToListAsync();
            return clubs;
        }

        public async Task<List<Club>> GetClubsByContinentalLeagueId(int continentalLeagueId)
        {
            var clubs = await _context.Clubs
                                      .Where(c => c.ContinentalLeagueId == continentalLeagueId)
                                      //.Include(c => c.DomesticLeague)
                                      //.Include(c => c.ContinentalLeague)
                                      .ToListAsync();
            return clubs;
        }
    }
}
