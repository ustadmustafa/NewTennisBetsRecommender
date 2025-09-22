using merzigo.bms.tennis.Services;
using Microsoft.AspNetCore.Mvc;

namespace merzigo.bms.tennis.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IApiService _apiService;

        public PlayersController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> GetPlayers()
        {
            var players = await _apiService.GetPlayers(1905);
            // Players listesini yazdır
            Console.WriteLine("=== Players ===");
            foreach (var p in players)
            {
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(
                    p, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
                ));
            }
            return View(players);
        }
    }
}
