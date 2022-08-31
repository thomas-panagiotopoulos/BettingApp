using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.DTOs
{
    public class RequestTopUpDTO
    {
        public decimal Amount { get; set; }

        public string CardNumber { get; set; }

        public string SecurityNumber { get; set; }

        public string CardHolderName { get; set; }

        public string ExpirationDateMM { get; set; }

        public string ExpirationDateYY { get; set; }

        public int CardTypeId { get; set; }

        public string CardTypeName { get; set; }
    }
}
