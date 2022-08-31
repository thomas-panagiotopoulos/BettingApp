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
    public class BettingService : IBettingService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _apiClient;
        private readonly IMapper _mapper;
        private readonly ILogger<BettingService> _logger;
        private readonly string _bettingUrl;

        public BettingService(IOptions<AppSettings> settings, 
                                 HttpClient apiClient,
                                 IMapper mapper,
                                 ILogger<BettingService> logger)
        {
            _settings = settings;
            _apiClient = apiClient;
            _mapper = mapper;
            _logger = logger;

            if (_settings.Value.IsContainerEnv == true)
            {
                _bettingUrl = _settings.Value.GamblingUrl;
            }
            else
            {
                _bettingUrl = _settings.Value.BettingUrl;
            }
        }

        public async Task<Bet> GetBet(string betId)
        {
            var uri = _settings.Value.IsContainerEnv ? API.Betting.GetBet(_bettingUrl, betId)
                                                     : APIv0.Betting.GetBet(_bettingUrl, betId);
            var response = await _apiClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var betDTO = JsonConvert.DeserializeObject<BetDTO>(responseString);
                var bet = _mapper.Map<Bet>(betDTO);

                return bet;
            }
            return null;
        }

        public async Task<IEnumerable<BetPreview>> GetBetPreviewsPage(int pageNumber)
        {
            var uri = _settings.Value.IsContainerEnv ? API.Betting.GetBetPreviewsPage(_bettingUrl, pageNumber)
                                                     : APIv0.Betting.GetBetPreviewsPage(_bettingUrl, pageNumber);
            var response = await _apiClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var betPreviewDTOs = JsonConvert.DeserializeObject<List<BetPreviewDTO>>(responseString);
                var betPreviewsPage = betPreviewDTOs?.Select(bpDTO => _mapper.Map<BetPreview>(bpDTO));

                return betPreviewsPage;
            }
            return null;
        }

        public async Task<int> GetBetPreviewsPagesCount()
        {
            var uri = _settings.Value.IsContainerEnv ? API.Betting.GetBetPreviewsPagesCount(_bettingUrl)
                                                     : APIv0.Betting.GetBetPreviewsPagesCount(_bettingUrl);
            var response = await _apiClient.GetAsync(uri);
            var responseString = await response.Content.ReadAsStringAsync();

            var betPreviewsPagesCount = JsonConvert.DeserializeObject<int>(responseString);

            return betPreviewsPagesCount;
        }

        public async Task<bool> CancelBet(string betId)
        {
            var uri = _settings.Value.IsContainerEnv ? API.Betting.CancelBet(_bettingUrl, betId)
                                                     : APIv0.Betting.CancelBet(_bettingUrl, betId);
            var response = await _apiClient.PostAsync(uri, null);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> RequestExists(string requestId)
        {
            var uri = _settings.Value.IsContainerEnv ? API.Betting.RequestExists(_bettingUrl, requestId)
                                                     : APIv0.Betting.RequestExists(_bettingUrl, requestId);
            var response = await _apiClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }
    }
}
