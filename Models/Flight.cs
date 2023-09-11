namespace FlightBookingAPI.Models
{
    public class Flight
    {
        public Flight(string flightNumber, string fromAirportCode, string toAirportCode, DateTime departureDate, DateTime returnDate, double price, FlightType flightType)
        {
            FlightNumber = flightNumber;
            FromAirportCode = fromAirportCode;
            ToAirportCode = toAirportCode;
            DepartureDate = departureDate;
            Price = price;
            FlightType = flightType;
        }

        public Flight(){}

        public string FlightNumber { get; set; }
        public string FromAirportCode { get; set; }
        public string ToAirportCode { get; set; }
        public DateTime DepartureDate { get; set; }
        public double Price { get; set; }
        public FlightType FlightType { get; set; }
        public TimeSpan EstimatedTravelTime { get; set; }
        public DateTime ArrivalTime { get; set; }
    }
}
