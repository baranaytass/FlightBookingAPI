using System.Text.Json.Serialization;

namespace FlightBookingAPI.Models
{
    public class FlightSearchRequest
    {
        public string FromAirportCode { get; set; }
        public string ToAirportCode { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public int PassengerCount { get; set; }
        public FlightType FlightType { get; set; }
    }

    public enum FlightType
    {
        OneWay,
        RoundTrip
    }
}
