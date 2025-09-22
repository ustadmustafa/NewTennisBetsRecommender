using merzigo.bms.tennis.Models.Odds;
using Microsoft.Extensions.Logging;
using merzigo.bms.tennis.Models.Livescore;

namespace merzigo.bms.tennis.Models
{
    public class HomeViewModel
    {
        public List<Livescore.Livescore> Livescores { get; set; } = new();
        public List<Events> Events { get; set; } = new();
        public long? SelectedEventTypeKey { get; set; }
    }
}
