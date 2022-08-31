using BettingApp.Services.Wallets.API.Infrastructure.Exceptions;
using BettingApp.Services.Wallets.API.Model.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.Model
{
    public class Wallet : IEntity
    {
        public string Id { get; private set; }

        public string GamblerId { get; private set; }

        public decimal Balance { get; private set; }

        public decimal PreviousBalance { get; private set; }

        public DateTime LastTimeUpdated { get; private set; }

        public string LastTransactionId { get; private set; }

        public int TotalTransactions { get; private set; }

        public decimal TotalWageredAmount { get; private set; }

        public decimal TotalWinningsAmount { get; private set; }

        public decimal TotalTopUpAmount { get; private set; }

        public decimal TotalWithdrawAmount { get; private set; }

        public List<Transaction> Transactions { get; private set; }

        

        // constructors
        protected Wallet()
        {
            Id = Guid.NewGuid().ToString();
            Balance = 0;
            PreviousBalance = 0;
            LastTimeUpdated = DateTime.UtcNow.AddHours(2);
            LastTransactionId = String.Empty;
            TotalTransactions = 0;
            TotalWageredAmount = 0;
            TotalWinningsAmount = 0;
            TotalTopUpAmount = 0;
            TotalWithdrawAmount = 0;
            Transactions = new List<Transaction>();
        }

        public Wallet(string gamblerId) : this()
        {
            GamblerId = gamblerId;
        }

        public Wallet(string gamblerId, decimal balance) : this(gamblerId)
        {
            Balance = balance;
        }


        // methods

        public void ApplyTopUpTransaction(decimal amount, string requestId)
        {
            var transactionTypeId = TransactionType.TopUp.Id;

            // idempotency check
            if (TransactionExists(transactionTypeId, requestId))
            {
                throw new WalletsDomainException("A same TopUp transaction already exists in the wallet.");
            }

            // business rules check
            if (amount <= 0)
            {
                throw new WalletsDomainException("TopUp amount cannot be zero or negative value.");
            }

            // Update TotalTopUpAmount
            TotalTopUpAmount += amount;

            // Apply the transaction
            ApplyTransaction(amount, transactionTypeId, requestId);
        }

        public void ApplyWithdrawTransaction(decimal amount, string requestId)
        {
            var transactionTypeId = TransactionType.Withdraw.Id;

            // idempotency check
            if (TransactionExists(transactionTypeId, requestId))
            {
                throw new WalletsDomainException("A same Withdraw transaction already exists in the wallet.");
            }

            // business rule checks
            if (amount <= 0)
            {
                throw new WalletsDomainException("Withdraw amount cannot be zero or negative value.");
            }

            if (amount > Balance)
            {
                throw new WalletsDomainException("Cannot withdraw amount greater than available wallet balance.");
            }

            if (!WithdrawPreservesWelcomeBonus(amount))
            {
                throw new WalletsDomainException("Cannot withdraw any unused WelcomeBonus amount."); 
            }

            // Update TotalWithdrawAmount
            TotalWithdrawAmount += amount;

            // Apply the transaction
            ApplyTransaction(amount, transactionTypeId, requestId);
        }

        public void ApplyBetPaymentTransaction(decimal amount, string betId)
        {
            var transactionTypeId = TransactionType.BetPayment.Id;

            // idempotency check
            if (TransactionExists(transactionTypeId, betId))
            {
                throw new WalletsDomainException("A same BetPayment transaction already exists in the wallet.");
            }

            // business rule check
            if (amount > Balance)
            {
                throw new WalletsDomainException("Available balance is not enough for this BetPayment.");
            }

            // Update TotalWageredAmount
            TotalWageredAmount += amount;

            // Apply the transaction
            ApplyTransaction(amount, transactionTypeId, betId);
        }

        public void ApplyBetWinningsTransaction(decimal amount, string betId)
        {
            var transactionTypeId = TransactionType.BetWinnings.Id;

            // idempotency check
            if (TransactionExists(transactionTypeId, betId))
            {
                throw new WalletsDomainException("A same BetWinnings transaction already exists in the wallet.");
            }

            // business rule checks
            if (!TransactionExists(TransactionType.BetPayment.Id, betId))
            {
                throw new WalletsDomainException("Cannot credit BetWinnings if no BetPayment with the same BetId is found.");
            }

            if (TransactionExists(TransactionType.BetRefund.Id, betId))
            {
                throw new WalletsDomainException("Cannot credit BetWinnings if a BetRefund with the same BetId already exists.");
            }

            // Update TotalWinningsAmount
            TotalWinningsAmount += amount;

            // Apply the transaction
            ApplyTransaction(amount, transactionTypeId, betId);
        }

        public void ApplyBetRefundTransaction(decimal amount, string betId)
        {
            var transactionTypeId = TransactionType.BetRefund.Id;

            // idempotency check
            if (TransactionExists(transactionTypeId, betId))
            {
                throw new WalletsDomainException("A same BetRefund transaction already exists in the wallet.");
            }

            // business rule checks
            if (!TransactionExists(TransactionType.BetPayment.Id, betId))
            {
                throw new WalletsDomainException("Cannot credit BetRefund if no BetPayment with the same BetId is found.");
            }

            if(FindTransaction(TransactionType.BetPayment.Id, betId).Amount != amount)
            {
                throw new WalletsDomainException("BetRefund amount does not match with initial BetPayment amount.");
            }

            if (TransactionExists(TransactionType.BetWinnings.Id, betId))
            {
                throw new WalletsDomainException("Cannot credit BetRefund if a BetWinnings with the same BetId already exists.");
            }

            // Update TotalWageredAmount
            TotalWageredAmount -= amount;

            // Apply the transaction
            ApplyTransaction(amount, transactionTypeId, betId);
        }

        public void ApplyWelcomeBonusTransaction(decimal amount, string gamblerId)
        {
            var transactionTypeId = TransactionType.WelcomeBonus.Id;

            // idempotency check
            if (TransactionExists(transactionTypeId, gamblerId))
            {
                throw new WalletsDomainException("There can be only one WelcomeBonus transaction per wallet.");
            }

            // business rule check
            if(!gamblerId.Equals(this.GamblerId))
            {
                throw new WalletsDomainException("Cannot apply WelcomeBonus transaction with a different GamblerId.");
            }

            // Apply the transaction
            ApplyTransaction(amount, transactionTypeId, gamblerId);
        }

        public bool WithdrawPreservesWelcomeBonus(decimal amount)
        {
            if (amount > Balance)
                throw new WalletsDomainException("Cannot withdraw amount greater than available wallet balance.");

            if (TransactionExists(TransactionType.WelcomeBonus.Id, this.GamblerId))
            {
                var welcomeBonusTransaction = FindTransaction(TransactionType.WelcomeBonus.Id, this.GamblerId);
                var balanceAfterWithdraw = Balance - amount;
                if ( balanceAfterWithdraw < (welcomeBonusTransaction.Amount - TotalWageredAmount)  )
                {
                    return false;
                }
            }

            return true;
        }


        private void ApplyTransaction(decimal amount, int transactionTypeId, string identifierId)
        {
            // update balance
            PreviousBalance = Balance;
            Balance = TransactionType.From(transactionTypeId).Apply(Balance, amount);
            LastTimeUpdated = DateTime.UtcNow.AddHours(2);

            // create transaction entity and add it to Transactions list
            var transaction = new Transaction(this.Id, amount, LastTimeUpdated, PreviousBalance, Balance,
                                              transactionTypeId, identifierId);
            Transactions.Add(transaction);

            // order Transactions list by descending creation date
            Transactions = Transactions.OrderByDescending(t => t.DateTimeCreated).ToList();

            // update total transactions count
            TotalTransactions = Transactions.Count();

            // updae last transaction Id
            LastTransactionId = transaction.Id;
        }

        private Transaction FindTransaction(int transactionTypeId, string identifierId)
        {
            var transaction = Transactions.SingleOrDefault(t => t.TransactionTypeId == transactionTypeId && 
                                                                t.IdentifierId.Equals(identifierId));

            return transaction;
        }

        public bool TransactionExists(int transactionTypeId, string identifierId)
        {
            var exists = Transactions.Any(t => t.TransactionTypeId == transactionTypeId && t.IdentifierId.Equals(identifierId));

            return exists;
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
