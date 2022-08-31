using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Infrastructure
{
    public static class APIv0
    {
        public static class Sportsbook
        {
            public static string GetMatchesByDate(string baseUri, string date)
                => $"{baseUri}/sportsbook/GetMatchesByDate?date={date}";

            public static string GetLeaguesByDate(string baseUri, string date)
                => $"{baseUri}/sportsbook/GetLeaguesByDate?date={date}";

            public static string GetMatchResults(string baseUri) => $"{baseUri}/sportsbook/GetMatchResults";

            public static string GetMatch(string baseUri, string matchId)
                => $"{baseUri}/sportsbook/GetMatch?matchId={matchId}";

            public static string GetSelection(string baseUri, string matchId, int matchResultId)
                => $"{baseUri}/sportsbook/GetSelection?matchId={matchId}&matchResultId={matchResultId}";

            public static string RequestToAddSelection(string baseUri, string matchId, int gamblerMatchResultId)
                => $"{baseUri}/sportsbook/RequestToAddSelection?matchId={matchId}&gamblerMatchResultId={gamblerMatchResultId}";
        }

        public static class Betslips
        {
            public static string GetBetslip(string baseUri) => $"{baseUri}/betslips/GetBetslip";
            
            public static string CheckAddSelection(string baseUri, string matchId, int matchResultId) 
                => $"{baseUri}/betslips/CheckAddSelection?matchId={matchId}&matchResultId={matchResultId}";

            public static string VerifyLatestAddition(string baseUri, string latestAdditionId)
                => $"{baseUri}/betslips/VerifyLatestAddition?latestAdditionId={latestAdditionId}";
            public static string AddSelection(string baseUri) => $"{baseUri}/betslips/AddSelection";
            
            public static string RemoveSelection(string baseUri, string selectionId) 
                => $"{baseUri}/betslips/RemoveSelection?selectionId={selectionId}";
            
            public static string UpdateWageredAmount(string baseUri, decimal wageredAmount)
                => $"{baseUri}/betslips/UpdateWageredAmount?wageredAmount={wageredAmount}";
            
            public static string ClearBetslip(string baseUri) => $"{baseUri}/betslips/ClearBetslip";
            
            public static string Checkout(string baseUri) => $"{baseUri}/betslips/Checkout";
            
            public static string GetWalletBalance(string baseUri) => $"{baseUri}/betslips/GetWalletBalance";

        }

        public static class Betting
        {
            public static string GetBet(string baseUri, string betId) => $"{baseUri}/betting/GetBet?betId={betId}";
            
            public static string GetBetPreviewsPage(string baseUri, int pageNumber)
                => $"{baseUri}/betting/GetBetPreviewsPage?pageNumber={pageNumber}";
            
            public static string GetBetPreviewsPagesCount(string baseUri) => $"{baseUri}/betting/GetBetPreviewsPagesCount";
            
            public static string CancelBet(string baseUri, string betId) => $"{baseUri}/betting/CancelBet?betId={betId}";
            
            public static string RequestExists(string baseUri, string requestId) => $"{baseUri}/betting/RequestExists?requestId={requestId}";

           
        }

        public static class Wallets
        {
            public static string GetWalletPreview(string baseUri) => $"{baseUri}/wallets/GetWalletPreview";
            
            public static string GetTransactionsPage(string baseUri, int pageNumber)
                => $"{baseUri}/wallets/GetTransactionsPage?pageNumber={pageNumber}";
            
            public static string GetTransactionsPagesCount(string baseUri) 
                => $"{baseUri}/wallets/GetTransactionsPagesCount";
            
            public static string RequestTopUp(string baseUri) => $"{baseUri}/wallets/RequestTopUp";
            
            public static string RequestWithdraw(string baseUri) => $"{baseUri}/wallets/RequestWithdraw";
            
            public static string TopUpRequestExists(string baseUri, string requestId) 
                => $"{baseUri}/wallets/TopUpRequestExists?requestId={requestId}";
            
            public static string WithdrawRequestExists(string baseUri, string requestId) 
                => $"{baseUri}/wallets/WithdrawRequestExists?requestId={requestId}";

        }

        public static class Notifications
        {
            public static string GetNotificationsPreview(string baseUri) 
                => $"{baseUri}/notifications/GetNotificationsPreview";
            
            public static string GetNotificationsPage(string baseUri, int pageNumber)
                => $"{baseUri}/notifications/GetNotificationsPage?pageNumber={pageNumber}";
            
            public static string ReadNotificationsPage(string baseUri, int pageNumber)
                => $"{baseUri}/notifications/ReadNotificationsPage?pageNumber={pageNumber}";
            
            public static string MarkAsReadAllNotifications(string baseUri)
                => $"{baseUri}/notifications/MarkAsReadAllNotifications";
            
            public static string GetNotificationsPagesCount(string baseUri)
                => $"{baseUri}/notifications/GetNotificationsPagesCount";

        }
    }
}
