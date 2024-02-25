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
        private static IDatabase _db;

        private static RedisCache instance;
        public static RedisCache GetRedisCache()
        {
            if (instance == null)
            {
                instance = new RedisCache();
                _db = GetDatabase();
            }
            return instance;
        }

        private RedisCache() { }
        public string Get(string key)
        {
            return _db.StringGet(key);
        }

        public void Set(string key, string value)
        {
            _db.StringSetAsync(key, value).Wait();
        }

        private static IDatabase GetDatabase()
        {
            var builder = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json")
                               .Build();

            // var connectionString = builder.GetConnectionString("redis");
            var connectionString = "localhost: 6379,abortConnect=false";
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(connectionString);
            return redis.GetDatabase();

        }
    }
}
