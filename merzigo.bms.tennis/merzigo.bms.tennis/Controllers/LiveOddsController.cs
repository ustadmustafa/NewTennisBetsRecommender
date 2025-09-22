using merzigo.bms.tennis.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace merzigo.bms.tennis.Controllers
{
    public class LiveOddsController : Controller
    {
        private readonly IApiService _apiService;

        public LiveOddsController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> GetLiveOdds()
        {
            // Örnek olarak matchKey veriyoruz, istersen diğer parametreleri de ekleyebilirsin
            var liveOdds = await _apiService.GetLiveOdds(matchKey: 11976653);

            // Debug amaçlı console'a yazdır
            foreach (var kvp in liveOdds.Result)
            {
                Console.WriteLine($"MatchKey: {kvp.Key}");
                Console.WriteLine(JsonSerializer.Serialize(kvp.Value));
            }

            // View'a modeli gönderiyoruz
            return View(liveOdds);
        }
    }
}
