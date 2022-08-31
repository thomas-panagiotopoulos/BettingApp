using AutoMapper;
using BettingApp.WebApps.WebRazorPages.Infrastructure;
using BettingApp.WebApps.WebRazorPages.Models;
using BettingApp.WebApps.WebRazorPages.Services.ModelDTOs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace BettingApp.WebApps.WebRazorPages.Services
{
    public class BetslipsService : IBetslipsService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _apiClient;
        private readonly IMapper _mapper;
        private readonly ILogger<BetslipsService> _logger;
        private readonly string _betslipsUrl;
        public BetslipsService(IOptions<AppSettings> settings, 
                                 HttpClient apiClient,
                                 IMapper mapper,
                                 ILogger<BetslipsService> logger)
        {
            _settings = settings;
            _apiClient = apiClient;
            _mapper = mapper;
            _logger = logger;

            if (_settings.Value.IsContainerEnv == true)
            {
                _betslipsUrl = _settings.Value.GamblingUrl;
            }
            else
            {
                _betslipsUrl = _settings.Value.BetslipsUrl;
            }
        }

        public async Task<Betslip> GetBetslip()
        {

            var uri = _settings.Value.IsContainerEnv ? API.Betslips.GetBetslip(_betslipsUrl) 
                                                     : APIv0.Betslips.GetBetslip(_betslipsUrl);
            var response = await _apiClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var betslipDTO = JsonConvert.DeserializeObject<BetslipDTO>(responseString);
                var betslip = _mapper.Map<Betslip>(betslipDTO);

                return betslip;
            }

            return null;
        }

        public async Task<bool> CheckAddSelection(string matchId, int matchResultId)
        {
            var uri = _settings.Value.IsContainerEnv ? API.Betslips.CheckAddSelection(_betslipsUrl, matchId, matchResultId)
                                                     : APIv0.Betslips.CheckAddSelection(_betslipsUrl, matchId, matchResultId);
            var response = await _apiClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var checkResult = JsonConvert.DeserializeObject<bool>(responseString);

                return checkResult;
            }
            return false;
        }

        public async Task<bool> VerifyLatestAddition(string latestAdditionId)
        {
            var uri = _settings.Value.IsContainerEnv ? API.Betslips.VerifyLatestAddition(_betslipsUrl, latestAdditionId)
                                                     : APIv0.Betslips.VerifyLatestAddition(_betslipsUrl, latestAdditionId);
            var response = await _apiClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
                return true;
            
            return false;
        }
        public async Task<Betslip> AddSelection(SelectionDTO selection)
        {
            var uri = _settings.Value.IsContainerEnv ? API.Betslips.AddSelection(_betslipsUrl)
                                                     : APIv0.Betslips.AddSelection(_betslipsUrl);
            var selectionDto = _mapper.Map<SelectionDTO>(selection);
            var selectionContent = new StringContent(JsonConvert.SerializeObject(selectionDto), System.Text.Encoding.UTF8, "application/json");
            var response = await _apiClient.PostAsync(uri, selectionContent);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var betslipDto = JsonConvert.DeserializeObject<BetslipDTO>(responseString);
                var betslip = _mapper.Map<Betslip>(betslipDto);

                return betslip;
            }
            return null;
        }

        public async Task<bool> RemoveSelection(string selectionId)
        {
            var uri = _settings.Value.IsContainerEnv ? API.Betslips.RemoveSelection(_betslipsUrl, selectionId)
                                                     : APIv0.Betslips.RemoveSelection(_betslipsUrl, selectionId);
            var response = await _apiClient.PostAsync(uri, null);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> UpdateWageredAmount(decimal wageredAmount)
        {
            var uri = _settings.Value.IsContainerEnv ? API.Betslips.UpdateWageredAmount(_betslipsUrl, wageredAmount)
                                                     : APIv0.Betslips.UpdateWageredAmount(_betslipsUrl, wageredAmount);
            var response = await _apiClient.PostAsync(uri, null);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<bool> ClearBetslip()
        {
            var uri = _settings.Value.IsContainerEnv ? API.Betslips.ClearBetslip(_betslipsUrl)
                                                     : APIv0.Betslips.ClearBetslip(_betslipsUrl);
            var response = await _apiClient.DeleteAsync(uri);

            if (response.IsSuccessStatusCode)
                return true;

            return false;
        }

        public async Task<string> Checkout()
        {
            var uri = _settings.Value.IsContainerEnv ? API.Betslips.Checkout(_betslipsUrl)
                                                     : APIv0.Betslips.Checkout(_betslipsUrl);
            var response = await _apiClient.PostAsync(uri, null);

            if (response.IsSuccessStatusCode)
            {
                var responseRequestId = await response.Content.ReadAsStringAsync();

                return responseRequestId;
            }
            return null;
        }

        public async Task<decimal> GetWalletBalance()
        {
            var uri = _settings.Value.IsContainerEnv ? API.Betslips.GetWalletBalance(_betslipsUrl)
                                                     : APIv0.Betslips.GetWalletBalance(_betslipsUrl);
            var response = await _apiClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();

                // If running localy, we convert the value to Decimal using the CultureInfo that the Betslips.API
                // server uses. If running on Docker container, we dont use any CultureInfo for the conversion, since
                // container environment (Linux) already applies it's cultural info to Betslips.API,
                // which is as we want it (using dot instead of comma as a decimal delimeter).
                return _settings.Value.IsContainerEnv 
                            ? Convert.ToDecimal(responseString)
                            : Convert.ToDecimal(responseString, new CultureInfo("el-gr"));
            }

            return -1m;
        }
    }
}
