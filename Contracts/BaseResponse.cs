using FlightBookingAPI.Models;
using System.Text.Json.Serialization;

namespace FlightBookingAPI.Contracts
{
    public abstract class BaseResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; }
    }
}
