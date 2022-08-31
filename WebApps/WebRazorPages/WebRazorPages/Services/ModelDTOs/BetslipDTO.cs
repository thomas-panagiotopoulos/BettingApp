using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Services.ModelDTOs
{
    public class BetslipDTO
    {
        public string GamblerId { get; set; }

        public decimal WageredAmount { get; set; }

        public List<SelectionDTO> SelectionDTOs { get; set; }

        public decimal TotalOdd { get; set; }

        public decimal PotentialWinnings { get; set; }

        public decimal PotentialProfit { get; set; }

        public bool IsBetable { get; set; }

        public int MaxSelectionsLimit { get; set; }
    }
}
