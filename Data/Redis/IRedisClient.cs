namespace FlightBookingAPI.Data.Redis
{
    public interface IRedisClient
    {
        public Task<T?> GetAsync<T>(string key);
        public Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry);
    }
}
