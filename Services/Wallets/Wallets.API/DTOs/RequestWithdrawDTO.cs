using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.DTOs
{
    public class RequestWithdrawDTO
    {
        public decimal Amount { get; set; }

        public string IBAN { get; set; }

        public string BankAccountHolder { get; set; }
    }
}
