using System.Text.Json.Serialization;

namespace merzigo.bms.tennis.Models
{
    public class EventsResponse
    {
        [JsonPropertyName("success")]
        public int Success { get; set; }

        [JsonPropertyName("result")]
        public List<Events> Result { get; set; } = new();
    }
    public class Events
    {
        [JsonPropertyName("event_type_key")]
        public long EventTypeKey { get; set; }

        [JsonPropertyName("event_type_type")]
        public string EventTypeType { get; set; }
    }
}
