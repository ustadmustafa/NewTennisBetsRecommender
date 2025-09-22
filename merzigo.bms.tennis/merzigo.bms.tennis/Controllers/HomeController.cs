using merzigo.bms.tennis.Models;
using merzigo.bms.tennis.Services;
using Microsoft.AspNetCore.Mvc;

namespace merzigo.bms.tennis.Controllers
{
    public class HomeController : Controller
    {
        private readonly IApiService _apiService;

        public HomeController(IApiService apiService)
        {
            _apiService = apiService;
        }


        public async Task<IActionResult> Index()
        {
            var events = await _apiService.GetEvents();

            var tournaments = await _apiService.GetTournaments(); 

            var livescore = await _apiService.GetLivescore();

            var upcoming = await _apiService.GetFixtures(DateTime.Now, DateTime.Now.AddDays(30));

            var vm = new HomeViewModel
            {
                Events = events ?? new List<Events>(),
                Livescores = livescore ?? new List<merzigo.bms.tennis.Models.Livescore.Livescore>(),
                Upcoming = upcoming ?? new List<Models.Fixtures>(),
            };

            return View(vm);
        }
    }
}
