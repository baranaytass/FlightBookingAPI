using FlightBookingAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightBookingAPI.Services.Flights
{
    public interface IFlightService
    {
        public Task<List<Flight>> GetFilteredFlights(string fromAirportCode, string toAirportCode, DateTime flightDate);
    }
}
