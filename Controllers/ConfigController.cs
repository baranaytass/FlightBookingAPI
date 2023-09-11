using FlightBookingAPI.Contracts;
using FlightBookingAPI.Models;
using FlightBookingAPI.Services.Airports;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConfigController : ControllerBase
    {
        private readonly ILogger<ConfigController> _logger;
        private readonly IConfiguration _configuration;

        public ConfigController( ILogger<ConfigController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult GetServiceConfiguration()
        {
            try
            {
                return Ok(new ApiConfigResponse()
                {
                    Success = true,
                    ApiConfig = new ApiConfig()
                    {
                        AirportCount = _configuration.GetValue<int>("Settings:AirportCount"),
                        FlightCountPerAirport = _configuration.GetValue<int>("Settings:FlightCountPerAirport"),
                        DateScope = _configuration.GetValue<int>("Settings:DateScope"),
                    }
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ApiConfigResponse()
                {
                    Success = false,
                    Errors = new List<string> { e.Message }
                });
            }

        }
    }
}
