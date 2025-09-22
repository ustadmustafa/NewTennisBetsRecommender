using merzigo.bms.tennis.Models;
using merzigo.bms.tennis.Models.Livescore;
using merzigo.bms.tennis.Models.Odds;
using Microsoft.Extensions.FileSystemGlobbing;
using System;
using System.Text.Json;

namespace merzigo.bms.tennis.Services
{
    public class ApiService : IApiService
    {
        //https://api.api-tennis.com/tennis/?method=get_events&APIkey=!_your_account_APIkey_!
        //https://api.api-tennis.com/tennis/?method=get_tournaments&APIkey=!_your_account_APIkey_!
        //https://api.api-tennis.com/tennis/?method=get_fixtures&APIkey=!_your_account_APIkey_!&date_start=2019-07-24&date_stop=2019-07-24
        //https://api.api-tennis.com/tennis/?method=get_livescore&APIkey=!_your_account_APIkey_!
        //https://api.api-tennis.com/tennis/?method=get_H2H&APIkey=!_your_account_APIkey_!&first_player_key=30&second_player_key=5
        //https://api.api-tennis.com/tennis/?method=get_standings&event_type=WTA&APIkey=!_your_account_APIkey_!
        //https://api.api-tennis.com/tennis/?method=get_players&player_key=137&APIkey=!_your_account_APIkey_!
        //https://api.api-tennis.com/tennis/?method=get_odds&match_key=159923&APIkey=!_your_account_APIkey_!
        //https://api.api-tennis.com/tennis/?method=get_live_odds&APIkey=!_your_account_APIkey_!

        private readonly string _baseUrl = "https://api.api-tennis.com/tennis/?method=";
        private string _apiKey;

        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _apiKey = "a8a78ad99e8bc0623fb22b8ae906133c12361eae083008375af2a362aaa3cb31";
            _httpClient = new HttpClient();
        }

        public async Task<List<Events>> GetEvents()
        {
            var url = $"{_baseUrl}get_events&APIkey={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var eventsResponse = JsonSerializer.Deserialize<EventsResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return eventsResponse?.Result ?? new List<Events>();
        }

        public async Task<List<Tournaments>> GetTournaments() 
        {
            var url = $"{_baseUrl}get_tournaments&APIkey={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var tournamentsResponse = JsonSerializer.Deserialize<TournamentsResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return tournamentsResponse?.Result ?? new List<Tournaments>();
        }

        public async Task<List<Fixtures>> GetFixtures(DateTime dateStart, DateTime dateStop, long? eventTypeKey = null, long? tournamentKey = null, string? tournamentSeason = null, long? matchKey = null, long? playerKey = null, string? timezone = null)
        {
            var url = $"{_baseUrl}get_fixtures&APIkey={_apiKey}" + $"&date_start={dateStart:yyyy-MM-dd}&date_stop={dateStop:yyyy-MM-dd}";

            if (eventTypeKey.HasValue) url += $"&event_type_key={eventTypeKey}";
            if (tournamentKey.HasValue) url += $"&tournament_key={tournamentKey}";
            if (!string.IsNullOrEmpty(tournamentSeason)) url += $"&tournament_season={tournamentSeason}";
            if (matchKey.HasValue) url += $"&match_key={matchKey}";
            if (playerKey.HasValue) url += $"&player_key={playerKey}";
            if (!string.IsNullOrEmpty(timezone)) url += $"&timezone={timezone}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var fixturesResponse = JsonSerializer.Deserialize<FixturesResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return fixturesResponse?.Result ?? new List<Fixtures>();
        }

        public async Task<List<Livescore>> GetLivescore(long? eventTypeKey = null, long? tournamentKey = null, long? matchKey = null, long? playerKey = null, string? timezone = null)
        {
            var url = $"{_baseUrl}get_livescore&APIkey={_apiKey}";

            if (eventTypeKey.HasValue) url += $"&event_type_key={eventTypeKey}";
            if (tournamentKey.HasValue) url += $"&tournament_key={tournamentKey}";
            if (matchKey.HasValue) url += $"&match_key={matchKey}";
            if (playerKey.HasValue) url += $"&player_key={playerKey}";
            if (!string.IsNullOrEmpty(timezone)) url += $"&timezone={timezone}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var livescoreResponse = JsonSerializer.Deserialize<LivescoreResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return livescoreResponse?.Result ?? new List<Livescore>();
        }

        public async Task<H2HResult> GetH2H(long firstPlayerKey, long secondPlayerKey)
        {
            var url = $"{_baseUrl}get_H2H&APIkey={_apiKey}&first_player_key={firstPlayerKey}&second_player_key={secondPlayerKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var h2hResponse = JsonSerializer.Deserialize<H2HResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            // Eğer null ise boş bir H2HResult dön
            return h2hResponse?.Result ?? new H2HResult();
        }

        public async Task<List<Standings>> GetStandings(string eventType)
        {
            var url = $"{_baseUrl}get_standings&APIkey={_apiKey}&event_type={eventType}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var standingsResponse = JsonSerializer.Deserialize<StandingsResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return standingsResponse?.Result ?? new List<Standings>();

        }

        public async Task<List<Player>> GetPlayers(long playerKey, long? tournamentKey = null)
        {
            var url = $"{_baseUrl}get_players&APIkey={_apiKey}&player_key={playerKey}";

            if (tournamentKey.HasValue) url += $"&tournement_key={tournamentKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var playersResponse = JsonSerializer.Deserialize<PlayersResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return playersResponse?.Result ?? new List<Player>();
        }

        public async Task<OddsResponse> GetOdds(long matchKey, DateTime? dateStart = null, DateTime? dateStop = null, long? eventTypeKey = null, long? tournamentKey = null)
        {
            var url = $"{_baseUrl}get_odds&APIkey={_apiKey}&match_key={matchKey}";
            if (dateStart.HasValue) url += $"&date_start={dateStart:yyyy-MM-dd}";
            if (dateStop.HasValue) url += $"&date_stop={dateStop:yyyy-MM-dd}";
            if (eventTypeKey.HasValue) url += $"&event_type_key={eventTypeKey}";
            if (tournamentKey.HasValue) url += $"&tournament_key={tournamentKey}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var json = await response.Content.ReadAsStringAsync();
            var oddsResponse = JsonSerializer.Deserialize<OddsResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return oddsResponse ?? new OddsResponse();
        }

        public async Task<LiveOddsResponse> GetLiveOdds(long? eventTypeKey = null,long? tournamentKey = null,long? matchKey = null,long? playerKey = null,string? timezone = null)
        {
            var url = $"{_baseUrl}get_live_odds&APIkey={_apiKey}";

            if (eventTypeKey.HasValue)
                url += $"&event_type_key={eventTypeKey}";
            if (tournamentKey.HasValue)
                url += $"&tournament_key={tournamentKey}";
            if (matchKey.HasValue)
                url += $"&match_key={matchKey}";
            if (playerKey.HasValue)
                url += $"&player_key={playerKey}";
            if (!string.IsNullOrEmpty(timezone))
                url += $"&timezone={timezone}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync();

            var liveOddsResponse = JsonSerializer.Deserialize<LiveOddsResponse>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return liveOddsResponse ?? new LiveOddsResponse();
        }


    }
}
