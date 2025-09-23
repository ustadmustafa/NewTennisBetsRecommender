using merzigo.bms.tennis.Models;
using merzigo.bms.tennis.Models.Livescore;
using merzigo.bms.tennis.Services;
using Microsoft.AspNetCore.Mvc;

namespace merzigo.bms.tennis.Controllers
{
    public class OddSuggestionController : Controller
    {
        private readonly IApiService _apiService;
        
        public OddSuggestionController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> SuggestOdd(long matchId, string player1, string player2, long player1Id, long player2Id)
        {
            var dateStart = DateTime.Now.AddYears(-5);
            var dateStop = DateTime.Now;
            var fixtures_for_player1 = await _apiService.GetFixtures(dateStart, dateStop, matchKey: matchId, playerKey: player1Id);
            var fixtures_for_player2 = await _apiService.GetFixtures(dateStart, dateStop, matchKey: matchId, playerKey: player2Id);

            var h2hResult = await _apiService.GetH2H(player1Id, player2Id);

            var standingsATP = await _apiService.GetStandings("ATP");
            var standingsWTA = await _apiService.GetStandings("WTA");
            var player1ATP = standingsATP.FirstOrDefault(p => p.playerKey == player1Id);
            var player2ATP = standingsATP.FirstOrDefault(p => p.playerKey == player2Id);
            var player1WTA = standingsWTA.FirstOrDefault(p => p.playerKey == player1Id);
            var player2WTA = standingsWTA.FirstOrDefault(p => p.playerKey == player2Id);

            // Null olmayanı seçiyoruz
            var finalPlayer1 = player1ATP ?? player1WTA;
            var finalPlayer2 = player2ATP ?? player2WTA;

            var players1 = await _apiService.GetPlayers(player1Id);
            var players2 = await _apiService.GetPlayers(player2Id);

            var odds = await _apiService.GetOdds(matchId);
            var liveOdds = await _apiService.GetLiveOdds(matchKey: matchId);

            var vm = new OddSuggestionViewModel
            {
                FixturesPlayer1 = fixtures_for_player1 ?? new List<Fixtures>(),
                FixturesPlayer2 = fixtures_for_player2 ?? new List<Fixtures>(),
                H2H = h2hResult,
                Player1Name = player1,
                Player2Name = player2,
                Player1Id = player1Id,
                Player2Id = player2Id,
                Player1Standing = finalPlayer1, // yeni ekleme
                Player2Standing = finalPlayer2,  // yeni ekleme
                Player1 = players1?.FirstOrDefault(),
                Player2 = players2?.FirstOrDefault(),
                Odds = odds,
                LiveOdds = liveOdds
            };

            return View(vm);
        }


        [HttpPost]
        public async Task<IActionResult> SuggestOdd()
        {

            return View();
        }



        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CalculateOdds(long matchId, string player1, string player2, long player1Id, long player2Id, double? s_player1 = null, double? s_player2 = null, double? player1_winrate = null, double? player2_winrate = null, long? h2h_winner = null)
        {
            double? p_base = 0;
            double? winrate_diff = 0;
            double? odd_prob = 0;
            double? p_set = 0;


            if (s_player1 > s_player2)
            {
                p_base = s_player1 / (s_player1 + s_player2);
                if (h2h_winner == player1Id)
                {
                    p_base += 0.05; // H2H galibiyeti varsa %5 ekle
                }

                if (player1_winrate > player2_winrate)
                {
                    winrate_diff = player1_winrate - player2_winrate;
                    winrate_diff = winrate_diff / 10;
                }
                p_base += winrate_diff;
                odd_prob = p_base;
                p_set = CalculateSetProbability(p_base);
                var p_2_0 = p_set * p_set;
                var p_2_1 = 2 * p_set * p_set * (1 - p_set);
                var p_0_2 = (1 - p_set) * (1 - p_set);
                var p_1_2 = 2 * (1 - p_set) * (1 - p_set) * p_set;

                Console.WriteLine($"{p_base} ihtimalle {player1} kazanma oranları:  P(2-0): {p_2_0}, P(2-1): {p_2_1}"); // Örnek çıktı
            }
            else
            {
                p_base = s_player2 / (s_player1 + s_player2);
                if (h2h_winner == player2Id)
                {
                    p_base += 0.05; // H2H galibiyeti varsa %5 ekle
                }

                if (player2_winrate > player1_winrate)
                {
                    winrate_diff = player2_winrate - player1_winrate;
                    winrate_diff = winrate_diff / 10;
                }
                p_base += winrate_diff;
                odd_prob = p_base;
                p_set = CalculateSetProbability(p_base);
                var p_2_0 = p_set * p_set;
                var p_2_1 = 2 * p_set * p_set * (1 - p_set);
                var p_0_2 = (1 - p_set) * (1 - p_set);
                var p_1_2 = 2 * (1 - p_set) * (1 - p_set) * p_set;

                Console.WriteLine($"{p_base} ihtimalle {player2} kazanma oranları:P(2-0): {p_2_0}, P(2-1): {p_2_1}"); // Örnek çıktı
            }

            var vm = new CalculateOddsViewModel
            {
                MatchId = matchId,
                Player1Name = player1,
                Player2Name = player2,
                Player1Id = player1Id,
                Player2Id = player2Id,
                SPlayer1 = s_player1,
                SPlayer2 = s_player2,
                Player1Winrate = player1_winrate,
                Player2Winrate = player2_winrate,
                H2HWinner = h2h_winner,
                FavoredPlayerId = (s_player1 >= s_player2 ? player1Id : player2Id),
                BaseMatchProbability = odd_prob,
                SetProbability = p_set,
                Probability_2_0 = p_set.HasValue ? p_set * p_set : null,
                Probability_2_1 = p_set.HasValue ? 2 * p_set * p_set * (1 - p_set) : null,
                Probability_0_2 = p_set.HasValue ? (1 - p_set) * (1 - p_set) : null,
                Probability_1_2 = p_set.HasValue ? 2 * (1 - p_set) * (1 - p_set) * p_set : null,
            };

            return View(vm);
        }

        private double CalculateSetProbability(double? p_match, double tolerance = 1e-6)
        {
            double low = 0.0;
            double high = 1.0;
            double mid = 0.0;

            while (high - low > tolerance)
            {
                mid = (low + high) / 2.0;
                double f_mid = Math.Pow(mid, 2) * (3 - 2 * mid); // p_match formula

                if (f_mid < p_match)
                    low = mid;
                else
                    high = mid;
            }

            return (low + high) / 2.0; // yaklaşık p_set
        }
    }


}


