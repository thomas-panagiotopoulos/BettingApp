using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Sportbook.API.DTOs
{
    public class SelectionDTO
    {
        public int GamblerMatchResultId { get; set; }
        public string GamblerMatchResultName { get; set; }
        public string GamblerMatchResultAliasName { get; set; }

        public decimal Odd { get; set; }
        public decimal InitialOdd { get; set; }

        public string RelatedMatchId { get; set; }

        public string HomeClubName { get; set; }

        public string AwayClubName { get; set; }

        public DateTime KickoffDateTime { get; set; }

        public string CurrentMinute { get; set; }

        public int HomeClubScore { get; set; }

        public int AwayClubScore { get; set; }

        public int RequirementTypeId { get; set; }

        public string RequirementTypeName { get; set; }

        public string RequirementTypeAliasName { get; set; }

        public decimal RequiredValue { get; set; }

        public bool IsBetable { get; set; }
    }
}
