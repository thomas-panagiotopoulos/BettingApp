using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.MatchSimulation.API.Application.DTOs
{
    public class PossiblePickDTO
    {
        public int MatchResultId { get; set; }
        public decimal Odd { get; set; }
        public int RequirementTypeId { get; set; }
        public decimal RequiredValue { get; set; }
    }
}
