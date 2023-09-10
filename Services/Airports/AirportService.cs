using FlightBookingAPI.Data.Redis;
using FlightBookingAPI.Models;
using Newtonsoft.Json;

namespace FlightBookingAPI.Services.Airports
{
    public class AirportService: IAirportService
    {
        private readonly IRedisClient _redisClient;

        public AirportService(IRedisClient redisClient)
        {
            _redisClient = redisClient;
        }

        public async Task<List<Airport>> GetAllAirports()
        {
            try
            {
                var airports = await _redisClient.GetAsync<List<Airport>>("Airports");

                if (airports is null || airports.Count == 0)
                {
                    throw new Exception("There is no airport loaded...");
                }

                return airports;
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}
