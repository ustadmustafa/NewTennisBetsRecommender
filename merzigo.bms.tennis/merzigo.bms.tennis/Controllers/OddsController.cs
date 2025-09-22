using merzigo.bms.tennis.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace merzigo.bms.tennis.Controllers
{
    public class OddsController : Controller
    {
        private readonly IApiService _apiService;

        public OddsController(IApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> GetOdds()
        {
            var odds = await _apiService.GetOdds(12069654);

            // Tüm response’u tek seferde JSON olarak yazdır
            Console.WriteLine("=== Full OddsResponse JSON ===");
            Console.WriteLine(JsonSerializer.Serialize(
                odds, new JsonSerializerOptions { WriteIndented = true }
            ));

            // Result içindeki her MatchOdds’u ayrı ayrı yazdır
            foreach (var kvp in odds.Result)
            {
                Console.WriteLine($"=== Match Key: {kvp.Key} ===");
                Console.WriteLine(JsonSerializer.Serialize(
                    kvp.Value, new JsonSerializerOptions { WriteIndented = true }
                ));
            }

            // Örnek: Home/Away oranlarını yazdır
            foreach (var kvp in odds.Result)
            {
                var matchOdds = kvp.Value;
                Console.WriteLine("--- Home Odds ---");
                foreach (var home in matchOdds.HomeAway.Home)
                {
                    Console.WriteLine($"{home.Key}: {home.Value}");
                }

                Console.WriteLine("--- Away Odds ---");
                foreach (var away in matchOdds.HomeAway.Away)
                {
                    Console.WriteLine($"{away.Key}: {away.Value}");
                }
            }

            return View(odds);
        }
    }
}
