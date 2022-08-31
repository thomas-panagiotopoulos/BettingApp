using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SimulationAggregate;
using BettingApp.Services.MatchSimulation.Domain.Seedwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Infrastructure.Repositories
{
    public class SimulationRepository : ISimulationRepository
    {
        private readonly MatchSimulationContext _context;

        public SimulationRepository(MatchSimulationContext context)
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

        public Simulation Add(Simulation simulation)
        {
            var entity = _context.Simulations.Add(simulation).Entity;

            return entity;
        }

        public void Update(Simulation simulation)
        {
            _context.Entry(simulation).State = EntityState.Modified;
        }

        public bool RemoveById(string simulationId)
        {
            var simulation = _context.Simulations.SingleOrDefault(s => s.Id.Equals(simulationId));

            if (simulation == null) return false;

            _context.Simulations.Remove(simulation);

            return true;
        }

        public async Task<List<Simulation>> GetAllAsync()
        {
            var simulations = await _context.Simulations
                                      .Include(s => s.Status)
                                      .ToListAsync();

            return simulations;

        }

        public async Task<List<Simulation>> GetByStatusIdAsync(int statusId)
        {
            var simulations = await _context.Simulations
                                            .Include(s => s.Status)
                                            .Where(s => s.StatusId == statusId)
                                            .ToListAsync();
            return simulations;
        }

        public async Task<Simulation> GetByIdAsync(string simulationId)
        {
            var simulation = await _context.Simulations
                                          .Include(s => s.Status)
                                          .SingleOrDefaultAsync(s => s.Id.Equals(simulationId));

            // If simulation is not found in the DbSet, search in the LocalView for newly added simulations.
            // Attention: cannot use async LINQ methods on Local as it doesn't support them
            if (simulation == null)
            {
                simulation = _context.Simulations
                                     .Local
                                     .AsQueryable()
                                     .Include(s => s.Status)
                                     .SingleOrDefault(s => s.Id.Equals(simulationId));
            }

            return simulation;
        }

        
    }
}
