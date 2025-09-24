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
        public async Task<IActionResult> SuggestOdd(long matchId, string player1, string player2, long player1Id, long player2Id, string? matchType = null)
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
                LiveOdds = liveOdds,
                MatchType = matchType
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
        public IActionResult CalculateOdds(long matchId, string player1, string player2, long player1Id, long player2Id, double? s_player1 = null, double? s_player2 = null, double? player1_winrate = null, double? player2_winrate = null, long? h2h_winner = null, string? matchType = null)
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

            // Optionally fetch odds to show on results page
            var odds = _apiService.GetOdds(matchId);
            var liveOdds = _apiService.GetLiveOdds(matchKey: matchId);

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
                Odds = odds?.Result,
                LiveOdds = liveOdds?.Result,
                MatchType = matchType,
            };

            return View(vm);
        }

        public async Task<IActionResult> CalculateLiveOdds(long matchId, string player1, string player2, long player1Id, long player2Id, double? s_player1 = null, double? s_player2 = null, double? player1_winrate = null, double? player2_winrate = null, long? h2h_winner = null, string? matchType = null)
        {

            var livescores = await _apiService.GetLivescore(matchKey: matchId);
            var liveOdds = await _apiService.GetLiveOdds(matchKey: matchId);

            double p_base = 0;
            double winrate_diff = 0;
            //double? odd_prob = 0;
            double p_set = 0;


            if (s_player1.HasValue && s_player2.HasValue)
            {
                if (s_player1 > s_player2)
                {
                    p_base = (double)(s_player1 / (s_player1 + s_player2));
                    if (h2h_winner == player1Id) p_base += 0.05;
                    if(player1_winrate > player2_winrate)
                    {
                        winrate_diff = (double)((player1_winrate - player2_winrate) / 10);
                        p_base += winrate_diff;
                    }
                }
                else
                {
                    p_base = (double)(s_player2 / (s_player1 + s_player2));
                    if (h2h_winner == player2Id) p_base += 0.05;
                    if(player2_winrate > player1_winrate)
                    {
                        winrate_diff = (double)((player2_winrate - player1_winrate) / 10);
                        p_base += winrate_diff;
                    }
                }             
            }

            if (livescores != null)
            {
                int setsP1 = livescores.SetsPlayer1;
                int setsP2 = livescores.SetsPlayer2;
                int gamesP1 = livescores.CurrentSetGamesPlayer1;
                int gamesP2 = livescores.CurrentSetGamesPlayer2;

                // Set bazlı avantaj
                if (setsP1 == 1 && setsP2 == 0)
                    p_base += 0.15 * (1 - p_base);
                else if (setsP2 == 1 && setsP1 == 0)
                    p_base -= 0.15 * p_base;

                // Oyun bazlı momentum (küçük etki)
                if (gamesP1 > gamesP2)
                    p_base += 0.02 * (1 - p_base);
                else if (gamesP2 > gamesP1)
                    p_base -= 0.02 * p_base;

                // Clamp (0.01–0.99 aralığında tut)
                p_base = Math.Clamp(p_base, 0.01, 0.99);
            }

            p_set = CalculateSetProbability(p_base);

            double? p_2_0 = p_set * p_set;
            double? p_2_1 = 2 * p_set * p_set * (1 - p_set);
            double? p_0_2 = (1 - p_set) * (1 - p_set);
            double? p_1_2 = 2 * (1 - p_set) * (1 - p_set) * p_set;

            if (livescores != null)
            {
                int setsP1 = livescores.SetsPlayer1;
                int setsP2 = livescores.SetsPlayer2;

                if (setsP1 == 1) // Player1 zaten 1 set aldı
                {
                    p_0_2 = null;
                    p_1_2 = null;
                }
                else if (setsP2 == 1) // Player2 zaten 1 set aldı
                {
                    p_2_0 = null;
                    p_2_1 = null;
                }
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
                BaseMatchProbability = p_base,
                SetProbability = p_set,
                Probability_2_0 = p_2_0,
                Probability_2_1 = p_2_1,
                Probability_0_2 = p_0_2,
                Probability_1_2 = p_1_2,
                Odds = null,
                LiveOdds = liveOdds,
                LiveScores = livescores,
                MatchType = matchType ?? "live"
            };

            return View("CalculateLiveOdds", vm);
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


