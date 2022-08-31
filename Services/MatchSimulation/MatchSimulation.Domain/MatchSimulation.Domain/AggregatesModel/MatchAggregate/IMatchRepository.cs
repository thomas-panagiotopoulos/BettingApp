using BettingApp.Services.MatchSimulation.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.Domain.AggregatesModel.MatchAggregate
{
    public interface IMatchRepository : IRepository<Match>
    {
        Match Add(Match match);

        Task<Match> GetByIdAsync(string matchId);

        Task<Match> GetBySimulationIdAsync(string simulationId);

        Task<List<Match>> GetMatchesByKickoffDate(DateTime date);

        Task<bool> ExistsWithKickoffDate(DateTime date);

        Task<bool> ExistsWithIdAsync(string matchId);
    }
}
