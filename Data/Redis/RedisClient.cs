using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace FlightBookingAPI.Data.Redis
{
    public class RedisClient : IRedisClient
    {
        private readonly IConnectionMultiplexer _redis;
        private readonly IDatabase _database;
        private readonly IConfiguration _config;

        public RedisClient(IConfiguration configuration)
        {
            _config = configuration ?? throw new ArgumentNullException(nameof(configuration));
            var redisConfig = ConfigurationOptions.Parse(_config.GetValue<string>("Redis:ConnectionString"));

            redisConfig.AbortOnConnectFail = false;
            redisConfig.ConnectTimeout = 30000;
            redisConfig.SyncTimeout = 30000;

            _redis = ConnectionMultiplexer.Connect(redisConfig);
            _database = _redis.GetDatabase();
        }

        public async Task<T> GetAsync<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));

            var value = await _database.StringGetAsync(key);

            if (!value.IsNullOrEmpty)
            {
                return JsonConvert.DeserializeObject<T>(value);
            }

            return default;
        }

        public async Task<bool> SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Key cannot be null or empty.", nameof(key));

            var serializedValue = JsonConvert.SerializeObject(value);
            return await _database.StringSetAsync(key, serializedValue, expiry);
        }
    }
}