using BettingApp.Services.MatchSimulation.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SimulationAggregate
{
    public interface ISimulationRepository : IRepository<Simulation>
    {
        Simulation Add(Simulation simulation);

        void Update(Simulation simulation);

        bool RemoveById(string simulationId);

        Task<List<Simulation>> GetAllAsync();

        Task<List<Simulation>> GetByStatusIdAsync(int statusId);

        Task<Simulation> GetByIdAsync(string simulationId);

    }
}
