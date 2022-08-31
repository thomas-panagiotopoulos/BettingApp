using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Services.ModelDTOs
{
    public class WalletPreviewDTO
    {
        public string Id { get; set; }
        public string GamblerId { get; set; }
        public decimal Balance { get; set; }
        public int TotalTransactions { get; set; }
        public decimal TotalWageredAmount { get; set; }
        public decimal TotalWinningsAmount { get; set; }
        public decimal TotalTopUpAmount { get; set; }
        public decimal TotalWithdrawAmount { get; set; }
    }
}
