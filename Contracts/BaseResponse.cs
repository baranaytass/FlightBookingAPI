using FlightBookingAPI.Models;
using System.Text.Json.Serialization;

namespace FlightBookingAPI.Contracts
{
    public class BaseResponse
    {
        [JsonPropertyName("success")]
        public bool Success { get; set; }

        [JsonPropertyName("data")]
        public object Data { get; set; }

        [JsonPropertyName("errors")]
        public List<string> Errors { get; set; }
    }
}
