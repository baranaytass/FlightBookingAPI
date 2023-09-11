using FlightBookingAPI.Models;

namespace FlightBookingAPI.Contracts
{
    public class ApiConfigResponse:BaseResponse
    {
        public ApiConfig ApiConfig { get; set; }
    }
}
