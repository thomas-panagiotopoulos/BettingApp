using BettingApp.Services.Wallets.API.Infrastructure.Exceptions;
using BettingApp.Services.Wallets.API.Model.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.Model
{
    public class TransactionType : IEnumeration
    {
        public static TransactionType TopUp = new TransactionType(1, nameof(TopUp), "requestId", (x,y) => x + y);
        public static TransactionType Withdraw = new TransactionType(2, nameof(Withdraw), "requestId", (x, y) => x - y);
        public static TransactionType BetPayment = new TransactionType(3, nameof(BetPayment), "betId", (x, y) => x - y);
        public static TransactionType BetWinnings = new TransactionType(4, nameof(BetWinnings), "betId", (x, y) => x + y);
        public static TransactionType BetRefund = new TransactionType(5, nameof(BetRefund), "betId", (x, y) => x + y);
        public static TransactionType WelcomeBonus = new TransactionType(6, nameof(WelcomeBonus), "gamblerId", (x, y) => x + y);

        public int Id { get; set; }
        public string Name { get; set; }
        public string IdentifierName { get; set; }
        public Func<decimal, decimal, decimal> Apply { get; set; }

        public TransactionType(int id, string name, string identifierName)
        {
            Id = id;
            Name = name;
            IdentifierName = identifierName;
        }

        public TransactionType(int id, string name, string identifierName, Func<decimal, decimal, decimal> apply)
            : this(id, name, identifierName)
        {
            Apply = apply;
        }


        public static IEnumerable<TransactionType> List() =>
            new[] { TopUp, Withdraw, BetPayment, BetWinnings, BetRefund, WelcomeBonus };

        public static TransactionType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new WalletsDomainException($"Possible values for TransactionType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static TransactionType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new WalletsDomainException($"Possible values for TransactionType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
