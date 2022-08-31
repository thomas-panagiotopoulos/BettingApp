using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Models
{
    public class BetPreview
    {
        public string Id { get; set; }

        public string GamblerId { get; set; }

        public DateTime DateTimeCreated { get; set; }

        public bool IsPaid { get; set; }

        public bool IsCancelable { get; set; }

        public int StatusId { get; set; }

        public string StatusName { get; set; }

        public int ResultId { get; set; }

        public string ResultName { get; set; }

        public decimal WageredAmount { get; set; }

        public decimal TotalOdd { get; set; }

        public decimal PotentialWinnings { get; set; }

        public decimal PotentialProfit { get; set; }

        public decimal InitialTotalOdd { get; set; }

        public decimal InitialPotentialWinnings { get; set; }

        public decimal InitialPotentialProfit { get; set; }

        public int SelectionsCount { get; set; }
    }
}
