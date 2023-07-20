using StackExchange.Redis;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace StockMarket.Api.Service;

public class CacheService : ICacheService
{
    private IDatabase _db;

    public CacheService(IConfiguration configuration)
    {
        ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configuration["RedisCnxString"] ??
                                                                    throw new ArgumentException("RedisCnxString cannot be null"));

        _db = redis.GetDatabase();
    }

    public async Task SetValue<T>(string key, T value) where T : class
    {
        string jsonString = JsonSerializer.Serialize(value);
        await _db.StringSetAsync(key, jsonString);
        
    }

    public async Task<T> GetValue<T>(string key) where T : class
    {
        string jsonString = await _db.StringGetAsync(key);
        if (string.IsNullOrEmpty(jsonString))
        {
            return default(T);
        }
        T value = JsonSerializer.Deserialize<T>(jsonString);
        return value;
    }
}
