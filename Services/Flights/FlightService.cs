using FlightBookingAPI.Data.Redis;
using FlightBookingAPI.Models;

namespace FlightBookingAPI.Services.Flights
{
    public class FlightService: IFlightService
    {
        private readonly IRedisClient _redisClient;

        public FlightService(IRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        public async Task<List<Flight>> GetFilteredFlights(string fromAirportCode, string toAirportCode, DateTime flightDate)
        {
            try
            {
                var flights = await _redisClient.GetAsync<List<Flight>>("Flights");

                if (flights is null || flights.Count == 0)
                    return new List<Flight>();

                var filteredFlights = flights.Where(f =>
                    f.FromAirportCode == fromAirportCode && f.ToAirportCode == toAirportCode &&
                    f.DepartureDate.Date == flightDate.Date).ToList();


                return filteredFlights;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);

                throw new Exception("An error occurred when FlightService.GetFilteredFlights()", e);
            }
        }
    }
}
