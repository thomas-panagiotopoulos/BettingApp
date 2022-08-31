using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.DTOs
{
    public class PossiblePickDTO
    {
        public string MatchId { get; set; }
        public int MatchResultId { get; set; }
        public string MatchResultName { get; set; }
        public string MatchResultAliasName { get; set; }
        public decimal Odd { get; set; }
        public decimal InitialOdd { get; set; }
        public int RequirementTypeId { get; set; }
        public string RequirementTypeName { get; set; }
        public string RequirementTypeAliasName { get; set; }
        public decimal RequiredValue { get; set; }

        public bool IsBetable { get; set; }
        public bool IsCanceled { get; set; }
    }
}
