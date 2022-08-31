using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Models
{
    public class Selection
    {
        public string Id { get; set; }

        public int GamblerMatchResultId { get; set; }
        public string GamblerMatchResultName { get; set; }
        public string GamblerMatchResultAliasName { get; set; }
        public string SelectionTypeName { get; set; }

        public decimal Odd { get; set; }
        public decimal InitialOdd { get; set; }

        public string RelatedMatchId { get; set; }

        public string HomeClubName { get; set; }

        public string AwayClubName { get; set; }

        public DateTime KickoffDateTime { get; set; }

        public string CurrentMinute { get; set; }

        public int HomeClubScore { get; set; }

        public int AwayClubScore { get; set; }

        public int RequirementTypeId { get; set; } // only for Betslips
        public string RequirementTypeName { get; set; } // only for Betslips
        public string RequirementTypeAliasName { get; set; } // only for Betslips
        public decimal RequiredValue { get; set; } // only for Betslips

        public bool IsBetable { get; set; } // only for Betslips
        public bool IsCanceled { get; set; } // Betslips need this value, Betting uses it too but also has the Status info
        
        public int StatusId { get; set; } // only for Betting
        public string StatusName { get; set; } // only for Betting

        public int ResultId { get; set; } // only for Betting
        public string ResultName { get; set; } // only for Betting
    }
}
