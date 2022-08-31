using BettingApp.Services.Wallets.API.DTOs;
using BettingApp.Services.Wallets.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingApp.Services.Wallets.API.Extensions
{
    public static class WalletExtensionMethods
    {
        public static WalletPreviewDTO ToWalletPreviewDTO(this Wallet wallet)
        {
            return new WalletPreviewDTO()
            {
                Id = wallet.Id,
                GamblerId = wallet.GamblerId,
                Balance = wallet.Balance,
                TotalTransactions = wallet.TotalTransactions,
                TotalWageredAmount = wallet.TotalWageredAmount,
                TotalWinningsAmount = wallet.TotalWinningsAmount,
                TotalTopUpAmount = wallet.TotalTopUpAmount,
                TotalWithdrawAmount = wallet.TotalWithdrawAmount,
            };
        }

        public static TransactionDTO ToTransactionDTO(this Transaction transaction)
        {
            return new TransactionDTO()
            {
                Id = transaction.Id,
                WalletId = transaction.WalletId,
                Amount = transaction.Amount,
                DateTimeCreated = transaction.DateTimeCreated,
                TransactionTypeId = transaction.TransactionTypeId,
                TransactionTypeName = AddSpacesToSentence(transaction.TransactionTypeName),
                IdentifierId = transaction.IdentifierId,
            };
        }

        private static string AddSpacesToSentence(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return "";
            StringBuilder newText = new StringBuilder(text.Length * 2);
            newText.Append(text[0]);
            for (int i = 1; i < text.Length; i++)
            {
                if (char.IsUpper(text[i]) && text[i - 1] != ' ')
                    newText.Append(' ');
                newText.Append(text[i]);
            }
            return newText.ToString();
        }
    }
}
