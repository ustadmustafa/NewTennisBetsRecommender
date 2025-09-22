using System.Text.Json.Serialization;

namespace merzigo.bms.tennis.Models.Livescore
{
    public class LivescoreResponse
    {
        [JsonPropertyName("success")]
        public int Success { get; set; }

        [JsonPropertyName("result")]
        public List<Livescore> Result { get; set; } = new();
    }

    public class Livescore
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
        public string? EventWinner { get; set; }

        [JsonPropertyName("event_status")]
        public string EventStatus { get; set; }

        [JsonPropertyName("event_type_type")]
        public string EventTypeType { get; set; }

        [JsonPropertyName("tournament_name")]
        public string TournamentName { get; set; }

        [JsonPropertyName("tournament_key")]
        public long TournamentKey { get; set; }

        [JsonPropertyName("tournament_round")]
        public string? TournamentRound { get; set; }

        [JsonPropertyName("tournament_season")]
        public string TournamentSeason { get; set; }

        [JsonPropertyName("event_live")]
        public string EventLive { get; set; }

        [JsonPropertyName("event_first_player_logo")]
        public string? EventFirstPlayerLogo { get; set; }

        [JsonPropertyName("event_second_player_logo")]
        public string? EventSecondPlayerLogo { get; set; }

        [JsonPropertyName("event_qualification")]
        public string EventQualification { get; set; }

        [JsonPropertyName("pointbypoint")]
        public List<PointByPoint> PointByPoint { get; set; } = new();

        [JsonPropertyName("scores")]
        public List<Score> Scores { get; set; } = new();
    }

    public class PointByPoint
    {
        [JsonPropertyName("set_number")]
        public string SetNumber { get; set; }

        [JsonPropertyName("number_game")]
        public string NumberGame { get; set; }

        [JsonPropertyName("player_served")]
        public string PlayerServed { get; set; }

        [JsonPropertyName("serve_winner")]
        public string ServeWinner { get; set; }

        [JsonPropertyName("serve_lost")]
        public string? ServeLost { get; set; }

        [JsonPropertyName("score")]
        public string Score { get; set; }

        [JsonPropertyName("points")]
        public List<Point> Points { get; set; } = new();
    }

    public class Point
    {
        [JsonPropertyName("number_point")]
        public string NumberPoint { get; set; }

        [JsonPropertyName("score")]
        public string Score { get; set; }

        [JsonPropertyName("break_point")]
        public string? BreakPoint { get; set; }

        [JsonPropertyName("set_point")]
        public string? SetPoint { get; set; }

        [JsonPropertyName("match_point")]
        public string? MatchPoint { get; set; }
    }

    public class Score
    {
        [JsonPropertyName("score_first")]
        public string ScoreFirst { get; set; }

        [JsonPropertyName("score_second")]
        public string ScoreSecond { get; set; }

        [JsonPropertyName("score_set")]
        public string ScoreSet { get; set; }
    }
}
