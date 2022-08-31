using BettingApp.Services.MatchSimulation.Domain.AggregatesModel.SimulationAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.DTOs
{
    public class SimulationDTO
    {
        public string MatchId { get; set; }
        public string HomeClubId { get; set; }
        public string AwayClubId { get; set; }
        public int LeagueId { get; set; }
        public DateTime KickoffDateTime { get; set; }
    }

    public class ClubDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int DomesticLeagueId { get; set; }
        public int ContinentalLeagueId { get; set; }
        public int AttackStat { get; set; }
        public int DefenceStat { get; set; }
        public int FormStat { get; set; }
    }
}
