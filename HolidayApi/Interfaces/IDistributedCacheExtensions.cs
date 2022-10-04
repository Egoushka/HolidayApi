using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace HolidayApi.Interfaces;

public interface IDistributedCacheExtensions
{
    public Task SetAsync<T>(IDistributedCache cache, string key, T value);
    public Task SetAsync<T>(IDistributedCache cache, string key, T value, DistributedCacheEntryOptions options);
    public bool TryGetValue<T>(IDistributedCache cache, string key, out T? value);

}