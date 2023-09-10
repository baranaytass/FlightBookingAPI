using FlightBookingAPI.Contracts;
using FlightBookingAPI.Models;
using FlightBookingAPI.Services.Airports;
using FlightBookingAPI.Services.Flights;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly ILogger<FlightsController> _logger;

        public FlightsController(IFlightService flightService, ILogger<FlightsController> logger)
        {
            _flightService = flightService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetFilteredFlights(
            [FromQuery] FlightSearchRequest flightSearchRequest)
        {
            try
            {
                var validator = new RequestValidator();
                var validationResult = validator.Validate(flightSearchRequest);

                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(new BaseResponse
                    {
                        Success = false,
                        Errors = errors
                    });
                }

                if (flightSearchRequest.FlightType == FlightType.OneWay)
                {
                    return Ok(new BaseResponse()
                    {
                        Success = true,
                        Data = _flightService.GetFilteredFlights(flightSearchRequest.FromAirportCode,
                            flightSearchRequest.ToAirportCode, flightSearchRequest.DepartureDate).Result
                    });
                }

                return Ok(new BaseResponse()
                {
                    Success = true,
                    Data =
                        new
                        {
                            DepartureFlights = _flightService.GetFilteredFlights(flightSearchRequest.FromAirportCode, flightSearchRequest.ToAirportCode, flightSearchRequest.DepartureDate).Result,
                            ReturnFlights = _flightService.GetFilteredFlights(flightSearchRequest.ToAirportCode, flightSearchRequest.FromAirportCode, (DateTime)flightSearchRequest.ReturnDate).Result,
                        }
                });
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, e.Message, e.InnerException?.Message);

                return BadRequest(new BaseResponse()
                {
                    Success = false,
                    Errors = new List<string> { e.Message }
                });
            }
        }
        
    }
}