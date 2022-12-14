using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Models
{
    public class Transaction
    {
        public string Id { get; set; }
        public string WalletId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DateTimeCreated { get; set; }
        public int TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; }
        public string IdentifierId { get; set; }

    }
}
