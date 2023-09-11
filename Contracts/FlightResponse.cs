using FlightBookingAPI.Models;
using System.Text.Json.Serialization;

namespace FlightBookingAPI.Contracts
{
    public class FlightResponse:BaseResponse
    {
        [JsonPropertyName("departureFlights")]
        public List<Flight> DepartureFlights { get; set; }

        [JsonPropertyName("returnFlights")]
        public List<Flight> ReturnFlights { get; set; }
    }
}
