using Bogus;
using FlightBookingAPI.Data.Redis;
using FlightBookingAPI.Models;
using Airport = FlightBookingAPI.Models.Airport;

namespace FlightBookingAPI.Data
{
    public class BogusMockDataGenerator
    {
        private readonly IRedisClient _redisClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<BogusMockDataGenerator> _logger;

        public BogusMockDataGenerator(IRedisClient redisClient, IConfiguration configuration, ILogger<BogusMockDataGenerator> logger)
        {
            _redisClient = redisClient ?? throw new ArgumentNullException(nameof(redisClient));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _logger = logger;
        }

        public void SeedData()
        {
            try
            {
                int airportCount = _configuration.GetValue<int>("Settings:AirportCount");
                int flightCountPerAirport = _configuration.GetValue<int>("Settings:FlightCountPerAirport");

                var airports = GenerateAirports(airportCount);
                var flights = GenerateFlights(flightCountPerAirport * airportCount, airports);

                _redisClient.SetAsync<List<Airport>>("Airports", airports, TimeSpan.MaxValue);
                _redisClient.SetAsync<List<Flight>>("Flights", flights, TimeSpan.MaxValue);
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message);
                throw;
            }
        }

        private List<Airport> GenerateAirports(int count)
        {
            var airportFaker = new Faker<Airport>()
                .RuleFor(a => a.Id, f => f.IndexFaker + 1)
                .RuleFor(a => a.Name, f => f.Address.City())
                .RuleFor(a => a.Code, f => f.Random.AlphaNumeric(3).ToUpper())
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.Country, f => f.Address.Country());

            return airportFaker.Generate(count);
        }

        private List<Flight> GenerateFlights(int count, List<Airport> airports)
        {
            var flightFaker = new Faker<Flight>()
                .RuleFor(f => f.FlightNumber, f => f.Random.AlphaNumeric(6))
                .RuleFor(f => f.FromAirportCode, f => f.PickRandom(airports).Code)
                .RuleFor(f => f.ToAirportCode, (f, flight) =>
                {
                    string toAirportCode;
                    do
                    {
                        toAirportCode = f.PickRandom(airports).Code;
                    } while (toAirportCode == flight.FromAirportCode);

                    return toAirportCode;
                })
                .RuleFor(f => f.DepartureDate,
                    f => f.Date.Soon(_configuration.GetValue<int>("Settings:DateScope"), DateTime.Today))
                .RuleFor(f => f.FlightType, f => FlightType.OneWay)
                .RuleFor(f => f.EstimatedTravelTime, f => f.Date.Timespan(TimeSpan.FromHours(11).Add(TimeSpan.FromHours(1))))
                .RuleFor(f => f.ArrivalTime, (f, flight) => flight.DepartureDate.Add(flight.EstimatedTravelTime))
                .RuleFor(f => f.Price, f => Math.Round(f.Random.Double(50, 500), 1));

            return flightFaker.Generate(count);
        }

    }
}
