using BettingApp.Services.MatchSimulation.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.AggregatesModel.ClubAggregate
{
    public interface IClubRepository : IRepository<Club>
    {
        Task<Club> GetByIdAsync(string clubId);

        Task<List<Club>> GetClubsByActiveSimulationIdAsync(string simulationId);

        Task<List<Club>> GetClubsByDomesticLeagueId(int domesticLeagueId);

        Task<List<Club>> GetClubsByContinentalLeagueId(int continentalLeagueId);
    }
}
