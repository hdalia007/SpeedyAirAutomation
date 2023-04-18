using Newtonsoft.Json;

namespace SpeedyAirAutomation.Model
{
    public class Flight
    {
        public int Id { get; set; }
        public int Day { get; set; }
        public string Departure { get; set; }
        public string Arrival { get; set; }
    }
}
