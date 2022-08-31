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
    public class SportsbookService : ISportsbookService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly HttpClient _apiClient;
        private readonly IMapper _mapper;
        private readonly ILogger<SportsbookService> _logger;
        private readonly string _sportsbookUrl;

        public SportsbookService(IOptions<AppSettings> settings,
                                 HttpClient apiClient, 
                                 IMapper mapper,
                                 ILogger<SportsbookService> logger)
        {
            _settings = settings;
            _apiClient = apiClient;
            _mapper = mapper;
            _logger = logger;

            if (_settings.Value.IsContainerEnv == true)
            {
                _sportsbookUrl = _settings.Value.GamblingUrl;
            }
            else
            {
                _sportsbookUrl = _settings.Value.SportsbookUrl;
            }
        }

        public async Task<List<Match>> GetMatchesByDate(DateTime date)
        {
            var uri = API.Sportsbook.GetMatchesByDate(_sportsbookUrl, date.ToString("o"));
            var response = await _apiClient.GetAsync(uri);
            var responseString = await response.Content.ReadAsStringAsync();

            var matchDTOs = JsonConvert.DeserializeObject<List<MatchDTO>>(responseString);
            var matches = matchDTOs?.Select(mDTO => _mapper.Map<Match>(mDTO)).ToList();

            return matches;
        }

        public async Task<List<League>> GetLeaguesByDate(DateTime date)
        {
            var uri = API.Sportsbook.GetLeaguesByDate(_sportsbookUrl, date.ToString("o"));
            var response = await _apiClient.GetAsync(uri);
            var responseString = await response.Content.ReadAsStringAsync();

            var leagueDTOs = JsonConvert.DeserializeObject<List<LeagueDTO>>(responseString);
            var leagues = leagueDTOs?.Select(lDTO => _mapper.Map<League>(lDTO)).ToList();

            return leagues;
        }

        public async Task<List<MatchResult>> GetMatchResults()
        {
            var uri = API.Sportsbook.GetMatchResults(_sportsbookUrl);
            var response = await _apiClient.GetAsync(uri);
            var responseString = await response.Content.ReadAsStringAsync();

            var matchResultDTOs = JsonConvert.DeserializeObject<List<MatchResultDTO>>(responseString);
            var matchResults = matchResultDTOs?.Select(mrDTO => _mapper.Map<MatchResult>(mrDTO)).ToList();

            return matchResults;
        }

        public async Task<Match> GetMatch(string matchId)
        {
            var uri = API.Sportsbook.GetMatch(_sportsbookUrl,matchId);
            var response = await _apiClient.GetAsync(uri);
           
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var matchDto = JsonConvert.DeserializeObject<MatchDTO>(responseString);
                var match = _mapper.Map<Match>(matchDto);

                return match;
            }
            
            return null;
        }

        public async Task<Selection> GetSelection(string matchId, int matchResultId)
        {
            var uri = API.Sportsbook.GetSelection(_sportsbookUrl, matchId, matchResultId);
            var response = await _apiClient.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var selectionDto = JsonConvert.DeserializeObject<SelectionDTO>(responseString);
                var selection = _mapper.Map<Selection>(selectionDto);

                return selection;
            }

            return null;
        }

        public async Task<string> RequestToAddSelection(string matchId, int gamblerMatchResultId)
        {
            var uri = API.Sportsbook.RequestToAddSelection(_sportsbookUrl,matchId,gamblerMatchResultId);
            var response = await _apiClient.PostAsync(uri, null);

            if (response.IsSuccessStatusCode)
            {
                var responseRequestId = await response.Content.ReadAsStringAsync();

                return responseRequestId;
            }

            return null;
        }
    }
}
