using BettingApp.Services.Wallets.API.Model.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.Model
{
    public class Card : IEntity
    {
        public string Id { get; private set; }

        public string GamblerId { get; private set; }

        public string Alias { get; private set; }

        public string CardNumber { get; private set; }

        public string SecurityNumber { get; private set; }

        public string CardHolderName { get; private set; }

        public DateTime Expiration { get; private set; }

        public CardType CardType { get; private set; }

        public int CardTypeId { get; private set; }

        public string CardTypeName { get; private set; }

        protected Card()
        {
            Id = Guid.NewGuid().ToString();
        }

        public Card(string gamblerId, string cardNumber, string securityNumber, string cardHolderName, DateTime expiration,
                    int cardTypeId)
            : this()
        {
            GamblerId = gamblerId;
            CardNumber = cardNumber;
            SecurityNumber = securityNumber;
            CardHolderName = cardHolderName;
            Expiration = expiration;
            CardTypeId = CardType.From(cardTypeId).Id;
            CardTypeName = CardType.From(cardTypeId).Name;
            Alias = CreateAlias();
        }

        public Card(string gamblerId, string cardNumber, string securityNumber, string cardHolderName, DateTime expiration,
                    string cardTypeName)
            : this()
        {
            GamblerId = gamblerId;
            CardNumber = cardNumber;
            SecurityNumber = securityNumber;
            CardHolderName = cardHolderName;
            Expiration = expiration;
            CardTypeId = CardType.FromName(cardTypeName).Id;
            CardTypeName = CardType.FromName(cardTypeName).Name;
            Alias = CreateAlias();
        }

        private string CreateAlias()
        {
            var alias = CardTypeName + " ****" + CardNumber[Math.Max(0, CardNumber.Length - 4)..];

            return alias;
        }


    }
}
