using BettingApp.Services.Betslips.Domain.Exceptions;
using BettingApp.Services.Betslips.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Betslips.Domain.AggregatesModel.WalletAggregate
{
    public class Wallet : Entity, IAggregateRoot
    {
        // Id

        public string GamblerId => _gamblerId;
        private string _gamblerId;

        public decimal Balance => _balance;
        private decimal _balance;

        public decimal PreviousBalance => _previousBalance;
        private decimal _previousBalance;

        public DateTime LastTimeUpdated => _lastTimeUpdated;
        private DateTime _lastTimeUpdated;

        // constructors

        protected Wallet()
        {
            Id = Guid.NewGuid().ToString();
            _balance = 0;
            _previousBalance = 0;
            _lastTimeUpdated = DateTime.UtcNow.AddHours(2);
        }

        public Wallet(string gamblerId) : this()
        {
            _gamblerId = gamblerId;
        }

        // class methods

        public void UpdateBalance(decimal newBalance, decimal oldBalance)
        {
            if(_balance != oldBalance)
            {
                // If we get here, it means WalletBalanceChangedIntegrationEvent's OldBalance value doesn't match
                // with Wallet's balance value. This could happen if a previous WalletBalanceChangedIntegrationEvent 
                // had been lost and thus Wallet's balance value is not up-to-date.
                // Uncomment next line you don't want to let the balance update happen in this case.

                //throw new BetslipsDomainException("Given old balance does not match with Wallet's current balance.");
                                                  
            }

            _previousBalance = _balance;
            _balance = newBalance;
            _lastTimeUpdated = DateTime.UtcNow.AddHours(2);
        }

        private static string RandomString(int length)
        {
            Random random = new Random();

            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                           "0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
