using System.Text.Json.Serialization;

namespace merzigo.bms.tennis.Models
{
    public class TournamentsResponse
    {
        [JsonPropertyName("success")]
        public int Success { get; set; }

        [JsonPropertyName("result")]
        public List<Tournaments> Result { get; set; } = new();
    }
    public class Tournaments
    {
        [JsonPropertyName("tournament_key")]
        public long TournamentKey { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("event_type_key")]
        public long EventTypeKey { get; set; }

        [JsonPropertyName("event_type_type")]
        public string EventTypeType { get; set; }
    }
}
