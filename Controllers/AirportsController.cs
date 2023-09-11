using FlightBookingAPI.Contracts;
using FlightBookingAPI.Services.Airports;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportsController : ControllerBase
    {
        private readonly IAirportService _airportService;
        private readonly ILogger<AirportsController> _logger;

        public AirportsController(IAirportService airportService, ILogger<AirportsController> logger)
        {
            _airportService = airportService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllAirports()
        {
            try
            {
                var airports = _airportService.GetAllAirports().Result;

                return Ok(new AirportResponse()
                {
                    Success = true,
                    Airports = airports
                });
            }
            catch (Exception e)
            {
                return BadRequest(new AirportResponse()
                {
                    Success = false,
                    Errors = new List<string> { e.Message }
                });
            }
            
        }
    }
}
