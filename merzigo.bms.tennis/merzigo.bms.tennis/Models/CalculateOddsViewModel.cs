namespace merzigo.bms.tennis.Models
{
    public class CalculateOddsViewModel
    {
        public long MatchId { get; set; }
        public string Player1Name { get; set; }
        public string Player2Name { get; set; }
        public long Player1Id { get; set; }
        public long Player2Id { get; set; }

        public double? SPlayer1 { get; set; }
        public double? SPlayer2 { get; set; }
        public double? Player1Winrate { get; set; }
        public double? Player2Winrate { get; set; }
        public long? H2HWinner { get; set; }

        public long FavoredPlayerId { get; set; }
        public double? BaseMatchProbability { get; set; }
        public double? SetProbability { get; set; }
        public double? Probability_2_0 { get; set; }
        public double? Probability_2_1 { get; set; }
        public double? Probability_0_2 { get; set; }
        public double? Probability_1_2 { get; set; }

        public merzigo.bms.tennis.Models.Odds.OddsResponse? Odds { get; set; }
        public merzigo.bms.tennis.Models.Odds.LiveOddsResponse? LiveOdds { get; set; }
        public System.Collections.Generic.List<merzigo.bms.tennis.Models.Livescore.Livescore>? LiveScores { get; set; }
        public string? MatchType { get; set; }
    }
}


