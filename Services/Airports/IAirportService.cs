using FlightBookingAPI.Models;

namespace FlightBookingAPI.Services.Airports
{
    public interface IAirportService
    {
        public Task<List<Airport>> GetAllAirports();
    }
}
