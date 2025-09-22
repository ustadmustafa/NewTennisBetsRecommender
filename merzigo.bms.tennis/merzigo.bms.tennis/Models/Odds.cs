using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace merzigo.bms.tennis.Models.Odds
{
    public class OddsResponse
    {
        [JsonPropertyName("success")]
        public int Success { get; set; }

        [JsonPropertyName("result")]
        public Dictionary<string, MatchOdds> Result { get; set; } = new();
    }

    public class MatchOdds
    {
        [JsonPropertyName("Home/Away")]
        public HomeAwayOdds HomeAway { get; set; } = new();

        [JsonPropertyName("Correct Score 1st Half")]
        public Dictionary<string, Dictionary<string, string>> CorrectScoreFirstHalf { get; set; } = new();

        [JsonPropertyName("Home/Away (1st Set)")]
        public HomeAwayOdds HomeAwayFirstSet { get; set; } = new();

        [JsonPropertyName("Set Betting")]
        public Dictionary<string, Dictionary<string, string>> SetBetting { get; set; } = new();

        [JsonPropertyName("Win In Straigh Sets (Player 1)")]
        public Dictionary<string, Dictionary<string, string>> WinStraightSetsPlayer1 { get; set; } = new();

        [JsonPropertyName("Win In Straigh Sets (Player 2)")]
        public Dictionary<string, Dictionary<string, string>> WinStraightSetsPlayer2 { get; set; } = new();
    }

    public class HomeAwayOdds
    {
        [JsonPropertyName("Home")]
        public Dictionary<string, string> Home { get; set; } = new();

        [JsonPropertyName("Away")]
        public Dictionary<string, string> Away { get; set; } = new();
    }
}
