using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.Infrastructure.Exceptions
{
    public class WalletsDomainException : Exception
    {
        public WalletsDomainException()
        { }

        public WalletsDomainException(string message)
            : base(message)
        { }

        public WalletsDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
