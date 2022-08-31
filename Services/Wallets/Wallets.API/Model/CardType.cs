using BettingApp.Services.Wallets.API.Infrastructure.Exceptions;
using BettingApp.Services.Wallets.API.Model.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.Model
{
    public class CardType : IEnumeration
    {
        public static CardType Visa = new CardType(1, "Visa");
        public static CardType Mastercard = new CardType(2, "Mastercard");
        public static CardType Amex = new CardType(3, "Amex");

        public int Id { get; set; }
        public string Name { get; set; }

        public CardType(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IEnumerable<CardType> List() =>
            new[] { Visa, Mastercard, Amex };

        public static CardType FromName(string name)
        {
            var state = List()
                .SingleOrDefault(s => String.Equals(s.Name, name, StringComparison.CurrentCultureIgnoreCase));

            if (state == null)
            {
                throw new WalletsDomainException($"Possible values for CardType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }

        public static CardType From(int id)
        {
            var state = List().SingleOrDefault(s => s.Id == id);

            if (state == null)
            {
                throw new WalletsDomainException($"Possible values for CardType: {String.Join(",", List().Select(s => s.Name))}");
            }

            return state;
        }
    }
}
