using AutoMapper;
using BettingApp.WebApps.WebRazorPages.Infrastructure;
using BettingApp.WebApps.WebRazorPages.Models;
using BettingApp.WebApps.WebRazorPages.Services.ModelDTOs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Services
{
    public class WalletsService : IWalletsService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _apiClient;
        private readonly IMapper _mapper;
        private readonly ILogger<WalletsService> _logger;
        private readonly string _walletsUrl;

        public WalletsService(IOptions<AppSettings> settings,
                                HttpClient apiClient,
                                 IMapper mapper,
                                 ILogger<WalletsService> logger)
        {
            _settings = settings;
            _apiClient = apiClient;
            _mapper = mapper;
            _logger = logger;

            if(_settings.Value.IsContainerEnv == true)
            {
                _walletsUrl = _settings.Value.WalletsAndNotificationsUrl;
            }
            else
            {
                _walletsUrl = _settings.Value.WalletsUrl;
            }
        }

        public async Task<WalletPreview> GetWalletPreview()
        {
            var uri = _settings.Value.IsContainerEnv ? API.Wallets.GetWalletPreview(_walletsUrl)
                                                     : APIv0.Wallets.GetWalletPreview(_walletsUrl);
            var response = await _apiClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var walletPreviewDTO = JsonConvert.DeserializeObject<WalletPreviewDTO>(responseString);
                var walletPreview = _mapper.Map<WalletPreview>(walletPreviewDTO);

                return walletPreview;
            }

            return null;
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsPage(int pageNumber)
        {
            var uri = _settings.Value.IsContainerEnv ? API.Wallets.GetTransactionsPage(_walletsUrl, pageNumber)
                                                     : APIv0.Wallets.GetTransactionsPage(_walletsUrl, pageNumber);
            var response = await _apiClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var transactionDTOs = JsonConvert.DeserializeObject<List<TransactionDTO>>(responseString);
                var transactionsPage = transactionDTOs?.Select(tDTO => _mapper.Map<Transaction>(tDTO));

                return transactionsPage;
            }
            return null;
        }

        public async Task<int> GetTransactionsPagesCount()
        {
            var uri = _settings.Value.IsContainerEnv ? API.Wallets.GetTransactionsPagesCount(_walletsUrl)
                                                     : APIv0.Wallets.GetTransactionsPagesCount(_walletsUrl);
            var response = await _apiClient.GetAsync(uri);
            var responseString = await response.Content.ReadAsStringAsync();

            var transactionsPagesCount = JsonConvert.DeserializeObject<int>(responseString);

            return transactionsPagesCount;
        }

        public async Task<string> RequestTopUp(decimal topUpAmount, Card card)
        {
            var uri = _settings.Value.IsContainerEnv ? API.Wallets.RequestTopUp(_walletsUrl)
                                                     : APIv0.Wallets.RequestTopUp(_walletsUrl);
            var requestTopUpDTO = new RequestTopUpDTO()
            {
                Amount = topUpAmount,
                CardNumber = card.CardNumber,
                SecurityNumber = card.SecurityNumber,
                CardHolderName = card.CardHolderName,
                ExpirationDateMM = card.ExpirationDateMM,
                ExpirationDateYY = card.ExpirationDateYY,
                CardTypeId = card.CardTypeId,
                CardTypeName = card.CardTypeName
            };
            var requestTopUpContent = new StringContent(JsonConvert.SerializeObject(requestTopUpDTO), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, requestTopUpContent);

            if (response.IsSuccessStatusCode)
            {
                var responseRequestId = await response.Content.ReadAsStringAsync();

                return responseRequestId;
            }

            return null;
        }

        public async Task<string> RequestWithdraw(decimal withdrawAmount, string iban, string bankAccountHolder)
        {
            var uri = _settings.Value.IsContainerEnv ? API.Wallets.RequestWithdraw(_walletsUrl)
                                                     : APIv0.Wallets.RequestWithdraw(_walletsUrl);
            var requestWithdrawDTO = new RequestWithdrawDTO()
            {
                Amount = withdrawAmount,
                IBAN = iban,
                BankAccountHolder = bankAccountHolder
            };
            var requestWithdrawContent = new StringContent(JsonConvert.SerializeObject(requestWithdrawDTO), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, requestWithdrawContent);

            if (response.IsSuccessStatusCode)
            {
                var responseRequestId = await response.Content.ReadAsStringAsync();

                return responseRequestId;
            }

            return null;
        }

        public async Task<bool> TopUpRequestExists(string requestId)
        {
            var uri = _settings.Value.IsContainerEnv ? API.Wallets.TopUpRequestExists(_walletsUrl, requestId)
                                                     : APIv0.Wallets.TopUpRequestExists(_walletsUrl, requestId);
            var response = await _apiClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> WithdrawRequestExists(string requestId)
        {
            var uri = _settings.Value.IsContainerEnv ? API.Wallets.WithdrawRequestExists(_walletsUrl, requestId)
                                                     : APIv0.Wallets.WithdrawRequestExists(_walletsUrl, requestId);
            var response = await _apiClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}
