using BettingApp.Services.Wallets.API.Model.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.Model
{
    public class Transaction : IEntity
    {
        public string Id { get; private set; }

        public string WalletId { get; private set; }

        public decimal Amount { get; private set; }

        public DateTime DateTimeCreated { get; private set; }

        public decimal WalletBalanceBefore { get; private set; }

        public decimal WalletBalanceAfter { get; private set; }

        public TransactionType TransactionType { get; private set; }

        public int TransactionTypeId { get; private set; }

        public string TransactionTypeName { get; private set; }

        public string IdentifierId { get; private set; }

        public string IdentifierName { get; private set; } // e.g. "BetId", "RequestId", etc

        // constructors
        protected Transaction()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Transaction(string walletId, decimal amount, DateTime dateTimeCreated, 
                            decimal walletBalanceBefore, decimal walletBalanceAfter,
                            int transactionTypeId, string identifierId) 
            : this()
        {
            WalletId = walletId;
            Amount = amount;
            DateTimeCreated = dateTimeCreated;
            WalletBalanceBefore = walletBalanceBefore;
            WalletBalanceAfter = walletBalanceAfter;
            TransactionTypeId = transactionTypeId;
            TransactionTypeName = TransactionType.From(transactionTypeId).Name;
            IdentifierId = identifierId;
            IdentifierName = TransactionType.From(transactionTypeId).IdentifierName;
        }


        // methods

    }
}
