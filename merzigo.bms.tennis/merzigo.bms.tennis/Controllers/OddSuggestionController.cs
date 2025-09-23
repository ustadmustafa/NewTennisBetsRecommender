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
        public IActionResult CalculateOdds(long matchId, string player1, string player2, long player1Id, long player2Id, double? s_player1 = null, double? s_player2 = null)
        {
            double? p_base = 0; 
            if (s_player1 > s_player2)
            {
                p_base = s_player1 / (s_player1 + s_player2);
            }
            else
            {
                p_base = s_player2 / (s_player1 + s_player2);
            }


             return RedirectToAction(nameof(SuggestOdd), new { matchId, player1, player2, player1Id, player2Id, s_player1, s_player2 });
        }
    }
}
