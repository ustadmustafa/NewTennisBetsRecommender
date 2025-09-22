using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace merzigo.bms.tennis.Models.Odds
{
    public class LiveOddsResponse
    {
        [JsonPropertyName("success")]
        public int Success { get; set; }

        [JsonPropertyName("result")]
        public Dictionary<string, MatchOddsDetail> Result { get; set; } = new();
    }

    public class MatchOddsDetail
    {
        [JsonPropertyName("event_key")]
        public long EventKey { get; set; }

        [JsonPropertyName("event_date")]
        public string EventDate { get; set; }

        [JsonPropertyName("event_time")]
        public string EventTime { get; set; }

        [JsonPropertyName("first_player_key")]
        public long FirstPlayerKey { get; set; }

        [JsonPropertyName("second_player_key")]
        public long SecondPlayerKey { get; set; }

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

        [JsonPropertyName("event_qualification")]
        public string EventQualification { get; set; }

        [JsonPropertyName("live_odds")]
        public List<LiveOdd> LiveOdds { get; set; } = new();
    }

    public class LiveOdd
    {
        [JsonPropertyName("odd_name")]
        public string OddName { get; set; }

        [JsonPropertyName("suspended")]
        public string Suspended { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }

        [JsonPropertyName("handicap")]
        public string Handicap { get; set; }

        [JsonPropertyName("upd")]
        public string Updated { get; set; }
    }
}
