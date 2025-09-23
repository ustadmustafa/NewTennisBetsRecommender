
namespace merzigo.bms.tennis.Models
{
    public class OddSuggestionViewModel
    {
        public List<Fixtures> FixturesPlayer1 { get; set; } = new();
        public List<Fixtures> FixturesPlayer2 { get; set; } = new();
        public H2HResult? H2H { get; set; }

        public string? Player1Name { get; set; }
        public string? Player2Name { get; set; }
        public long Player1Id { get; set; }
        public long Player2Id { get; set; }

        public Standings? Player1Standing { get; set; }
        public Standings? Player2Standing { get; set; }

        public Player? Player1 { get; set; }
        public Player? Player2 { get; set; }

        public merzigo.bms.tennis.Models.Odds.OddsResponse? Odds { get; set; }
        public merzigo.bms.tennis.Models.Odds.LiveOddsResponse? LiveOdds { get; set; }

        public string? MatchType { get; set; }
    }
}
