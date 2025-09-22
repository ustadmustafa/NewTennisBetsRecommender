using merzigo.bms.tennis.Models;
using merzigo.bms.tennis.Models.Livescore;
using merzigo.bms.tennis.Models.Odds;

namespace merzigo.bms.tennis.Services
{
    public interface IApiService
    {
        Task<List<Events>> GetEvents();
        Task<List<Tournaments>> GetTournaments();
        Task<List<Fixtures>> GetFixtures(DateTime dateStart, DateTime dateStop, long? eventTypeKey = null, long? tournamentKey = null, string? tournamentSeason = null, long? matchKey = null, long? playerKey = null, string? timezone = null);
        Task<List<Livescore>> GetLivescore(long? eventTypeKey = null, long? tournamentKey = null, long? matchKey = null, long? playerKey = null, string? timezone = null);
        Task<H2HResult> GetH2H(long firstPlayerKey, long secondPlayerKey);
        Task<List<Standings>> GetStandings(string eventType);
        Task<List<Player>> GetPlayers(long playerKey, long? tournamentKey = null);
        Task<OddsResponse> GetOdds(long matchKey, DateTime? dateStart = null, DateTime? dateStop = null, long? eventTypeKey = null, long? tournamentKey = null);
        Task<LiveOddsResponse> GetLiveOdds(long? eventTypeKey = null, long? tournamentKey = null, long? matchKey = null, long? playerKey = null, string? timezone = null);
    }
}
