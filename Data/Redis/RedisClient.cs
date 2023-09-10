using Newtonsoft.Json;
using StackExchange.Redis;

namespace FlightBookingAPI.Data.Redis
{
    public class RedisClient : IRedisClient
    {
        private readonly IConnectionMultiplexer _redis;

        public RedisClient(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }
        
        public async Task<T> GetAsync<T>(string key)
        {
            var db = _redis.GetDatabase();
            var value = await db.StringGetAsync(key);
            
            if (!value.IsNullOrEmpty)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            
            return default;
        }
        
        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var db = _redis.GetDatabase();
            var serializedValue = JsonConvert.SerializeObject(value);
            
            return await db.StringSetAsync(key, serializedValue, expiry);
        }
    }
}
