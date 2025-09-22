using System.Text.Json.Serialization;

namespace merzigo.bms.tennis.Models
{
    public class StandingsResponse
    {
        [JsonPropertyName("success")]
        public int Success { get; set; }

        [JsonPropertyName("result")]
        public List<Standings> Result { get; set; } = new();
    }
    public class Standings
    {
        [JsonPropertyName("place")]
        public string place { get; set; }

        [JsonPropertyName("player")]
        public string player { get; set; }

        [JsonPropertyName("player_key")]
        public long? playerKey { get; set; }

        [JsonPropertyName("league")]
        public string league { get; set; }
        [JsonPropertyName("movement")]
        public string movement { get; set; }
        [JsonPropertyName("country")]
        public string country { get; set; }
        [JsonPropertyName("points")]
        public string points { get; set; }

    }
}
