using merzigo.bms.tennis.Models;
using merzigo.bms.tennis.Services;
using Microsoft.AspNetCore.Mvc;

namespace merzigo.bms.tennis.Controllers
{
    public class FixturesController : Controller
    {
        private readonly IApiService _apiService;

        public FixturesController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> GetFixtures()
        {
            DateTime dateStart = new DateTime(2019, 07, 24);
            DateTime dateStop = new DateTime(2019, 07, 24);
            ////long? eventTypeKey = 215;          // Örnek Event Type Key (opsiyonel)
            //long? tournamentKey = 2398;      // Örnek Turnuva Key (opsiyonel)
            //string? tournamentSeason = "2019"; // Opsiyonel
            //long? matchKey = 44209;           // Opsiyonel, test için null
            //long? playerKey = 9645;          // Opsiyonel, test için null
            //string? timezone = "America/New_York";

            var fixtures = await _apiService.GetFixtures(dateStart,dateStop);

            foreach (var f in fixtures)
            {
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(f));
            }
            return View(fixtures);
        }
    }
}
