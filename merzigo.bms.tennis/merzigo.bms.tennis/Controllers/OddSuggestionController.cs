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

            var vm = new OddSuggestionViewModel
            {
                FixturesPlayer1 = fixtures_for_player1 ?? new List<Fixtures>(),
                FixturesPlayer2 = fixtures_for_player2 ?? new List<Fixtures>(),
                H2H = h2hResult,
                Player1Name = player1,
                Player2Name = player2,
                Player1Id = player1Id,
                Player2Id = player2Id
            };

            return View(vm);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
