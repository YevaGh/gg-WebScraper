using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace RateAmLib.Utils
{
    public interface IRedisCache
    {
        void Set(string key, string value);
        string Get(string key);
    }
    public class RedisCache : IRedisCache
    {
        public IDatabase _db;

        public RedisCache(IConfiguration builder)
        {
            var connectionString = builder.GetConnectionString("redis");
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);
            _db = redis.GetDatabase();
        }
        public string Get(string key)
        {
            return _db.StringGet(key);
        }

        public void Set(string key, string value)
        {
            _db.StringSetAsync(key, value).Wait();
        }

    }
}
