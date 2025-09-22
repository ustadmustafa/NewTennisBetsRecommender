using merzigo.bms.tennis.Services;
using Microsoft.AspNetCore.Mvc;

namespace merzigo.bms.tennis.Controllers
{
    public class StandingsController : Controller
    {
        private readonly IApiService _apiService;

        public StandingsController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> GetStandings()
        {
            var standings = await _apiService.GetStandings("WTA");

            // Standings listesini yazdır
            Console.WriteLine("=== Standings ===");
            foreach (var s in standings)
            {
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(
                    s, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
                ));
            }

            return View(standings);
        }
    }
}
