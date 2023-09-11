using FlightBookingAPI.Models;
using System.Text.Json.Serialization;

namespace FlightBookingAPI.Contracts
{
    public class AirportResponse: BaseResponse
    {
        [JsonPropertyName("airports")]
        public List<Airport> Airports { get; set; }
    }
}
