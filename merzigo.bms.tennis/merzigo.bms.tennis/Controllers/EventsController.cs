using merzigo.bms.tennis.Services;
using Microsoft.AspNetCore.Mvc;

namespace merzigo.bms.tennis.Controllers
{
    public class EventsController : Controller
    {
        private readonly IApiService _apiService;

        public EventsController(IApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<IActionResult> GetEvents()
        {
            var events = await _apiService.GetEvents();
            foreach (var e in events)
            {
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(e));
            }

            return View(events);
        }
    }
}
