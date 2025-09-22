using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace merzigo.bms.tennis.Models
{
    public class PlayersResponse
    {
        [JsonPropertyName("success")]
        public int Success { get; set; }

        [JsonPropertyName("result")]
        public List<Player> Result { get; set; }
    }

    public class Player
    {
        [JsonPropertyName("player_key")]
        public long PlayerKey { get; set; }

        [JsonPropertyName("player_name")]
        public string PlayerName { get; set; }

        [JsonPropertyName("player_country")]
        public string PlayerCountry { get; set; }

        [JsonPropertyName("player_bday")]
        public string PlayerBday { get; set; }

        [JsonPropertyName("player_logo")]
        public string PlayerLogo { get; set; }

        [JsonPropertyName("stats")]
        public List<PlayerStat> Stats { get; set; }
    }

    public class PlayerStat
    {
        [JsonPropertyName("season")]
        public string Season { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("rank")]
        public string Rank { get; set; }

        [JsonPropertyName("titles")]
        public string Titles { get; set; }

        [JsonPropertyName("matches_won")]
        public string MatchesWon { get; set; }

        [JsonPropertyName("matches_lost")]
        public string MatchesLost { get; set; }

        [JsonPropertyName("hard_won")]
        public string HardWon { get; set; }

        [JsonPropertyName("hard_lost")]
        public string HardLost { get; set; }

        [JsonPropertyName("clay_won")]
        public string ClayWon { get; set; }

        [JsonPropertyName("clay_lost")]
        public string ClayLost { get; set; }

        [JsonPropertyName("grass_won")]
        public string GrassWon { get; set; }

        [JsonPropertyName("grass_lost")]
        public string GrassLost { get; set; }
    }
}
