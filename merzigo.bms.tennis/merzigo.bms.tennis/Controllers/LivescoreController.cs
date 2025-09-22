using merzigo.bms.tennis.Models;
using merzigo.bms.tennis.Services;
using Microsoft.AspNetCore.Mvc;

namespace merzigo.bms.tennis.Controllers
{
    public class LivescoreController : Controller
    {
        private readonly IApiService _apiService;

        public LivescoreController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> GetLivescore()
        {
            ///*long? eventTypeKey = 215;*/          // Örnek Event Type Key (opsiyonel)
            //long? tournamentKey = 11734;      // Örnek Turnuva Key (opsiyonel)
            //long? matchKey = 12071881;           // Opsiyonel, test için null
            //long? playerKey = 55153;          // Opsiyonel, test için null
            //string? timezone = "America/New_York";

            var livescore = await _apiService.GetLivescore();

            foreach (var l in livescore)
            {
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(l));
            }

            return View(livescore);

        }
    }
}
