using merzigo.bms.tennis.Models;
using merzigo.bms.tennis.Services;
using Microsoft.AspNetCore.Mvc;

namespace merzigo.bms.tennis.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly IApiService _apiService;

        public TournamentsController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> GetTournaments()
        {
            var tournaments = await _apiService.GetTournaments();

            foreach (var t in tournaments)
            {
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(t));
            }

            return View(tournaments);
        }
    }
}
