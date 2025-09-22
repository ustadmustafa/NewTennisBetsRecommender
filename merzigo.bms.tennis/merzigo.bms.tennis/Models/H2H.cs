using System.Text.Json.Serialization;

namespace merzigo.bms.tennis.Models
{
    public class H2HResponse
    {
        [JsonPropertyName("success")]
        public int Success { get; set; }

        [JsonPropertyName("result")]
        public H2HResult Result { get; set; }
    }

    public class H2HResult
    {
        [JsonPropertyName("H2H")]
        public List<H2HMatch> H2H { get; set; } = new();

        [JsonPropertyName("firstPlayerResults")]
        public List<H2HMatch> FirstPlayerResults { get; set; } = new();

        [JsonPropertyName("secondPlayerResults")]
        public List<H2HMatch> SecondPlayerResults { get; set; } = new();
    }

    public class H2HMatch
    {
        [JsonPropertyName("event_key")]
        public long EventKey { get; set; }

        [JsonPropertyName("event_date")]
        public string EventDate { get; set; }

        [JsonPropertyName("event_time")]
        public string EventTime { get; set; }

        [JsonPropertyName("event_first_player")]
        public string EventFirstPlayer { get; set; }

        [JsonPropertyName("first_player_key")]
        public long FirstPlayerKey { get; set; }

        [JsonPropertyName("event_second_player")]
        public string EventSecondPlayer { get; set; }

        [JsonPropertyName("second_player_key")]
        public long SecondPlayerKey { get; set; }

        [JsonPropertyName("event_final_result")]
        public string EventFinalResult { get; set; }

        [JsonPropertyName("event_game_result")]
        public string EventGameResult { get; set; }

        [JsonPropertyName("event_serve")]
        public string EventServe { get; set; }

        [JsonPropertyName("event_winner")]
        public string EventWinner { get; set; }

        [JsonPropertyName("event_status")]
        public string EventStatus { get; set; }

        [JsonPropertyName("event_type_type")]
        public string EventTypeType { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("tournament_key")]
        public long TournamentKey { get; set; }

        [JsonPropertyName("tournament_round")]
        public string TournamentRound { get; set; }

        [JsonPropertyName("tournament_season")]
        public string TournamentSeason { get; set; }

        [JsonPropertyName("event_live")]
        public string EventLive { get; set; }

        [JsonPropertyName("event_first_player_logo")]
        public string EventFirstPlayerLogo { get; set; }

        [JsonPropertyName("event_second_player_logo")]
        public string EventSecondPlayerLogo { get; set; }
    }
}
