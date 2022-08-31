using System;

namespace BettingApp.Services.Betslips.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class BetslipsDomainException : Exception
    {
        public BetslipsDomainException()
        { }

        public BetslipsDomainException(string message)
            : base(message)
        { }

        public BetslipsDomainException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}
