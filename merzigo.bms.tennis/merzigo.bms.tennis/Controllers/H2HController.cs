using merzigo.bms.tennis.Models;
using merzigo.bms.tennis.Services;
using Microsoft.AspNetCore.Mvc;

namespace merzigo.bms.tennis.Controllers
{
    public class H2HController : Controller
    {
        private readonly IApiService _apiService;

        public H2HController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> GetH2H()
        {
            var h2hResult = await _apiService.GetH2H(13676, 5);
            // H2H listesini yazdır
            Console.WriteLine("=== H2H ===");
            foreach (var h in h2hResult.H2H)
            {
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(
                    h, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
                ));
            }

            // FirstPlayerResults listesini yazdır
            Console.WriteLine("=== First Player Results ===");
            foreach (var h in h2hResult.FirstPlayerResults)
            {
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(
                    h, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
                ));
            }

            // SecondPlayerResults listesini yazdır
            Console.WriteLine("=== Second Player Results ===");
            foreach (var h in h2hResult.SecondPlayerResults)
            {
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(
                    h, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
                ));
            }

            // Komple JSON olarak tek seferde yazdırmak istersen
            Console.WriteLine("=== Full H2HResult JSON ===");
            Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(
                h2hResult, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }
            ));

            return View(h2hResult);
        }
    }
}
