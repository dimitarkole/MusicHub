namespace MusicHub.Web.Tests.Mocks
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using Microsoft.Extensions.Caching.Distributed;

    public class DistributedCacheMock : IDistributedCache
    {
        public readonly ConcurrentDictionary<string, CacheEntry> cache = new ConcurrentDictionary<string, CacheEntry>();

        public byte[] Get(string key)
        {
            var keys = this.cache.Keys.ToList();
            foreach (string currentKey in keys)
            {
                CacheEntry cacheEntry = this.cache[currentKey];
                if (cacheEntry.AbsoluteExpiration.HasValue)
                {
                    DateTimeOffset absoluteExpiration = cacheEntry.AbsoluteExpiration.Value;
                    if (absoluteExpiration < DateTime.UtcNow)
                    {
                        this.cache.TryRemove(currentKey, out _);
                    }
                }
            }

            if (this.cache.ContainsKey(key))
            {
                CacheEntry cacheEntry = this.cache[key];
            }

            return null;
        }

        public Task<byte[]> GetAsync(string key, CancellationToken token = default) =>
            Task.FromResult(this.Get(key));

        public void Refresh(string key)
        {
        }

        public Task RefreshAsync(string key, CancellationToken token = default) => Task.CompletedTask;

        public void Remove(string key) => this.cache.TryRemove(key, out _);

        public Task RemoveAsync(string key, CancellationToken token = default)
        {
            this.Remove(key);
            return Task.CompletedTask;
        }

        public void Set(string key, byte[] value, DistributedCacheEntryOptions options)
        {
            var cacheEntry = new CacheEntry
            {
                AbsoluteExpiration = options.AbsoluteExpiration,
                Value = value,
                ExpiresAtTime = options.AbsoluteExpiration,
                SlidingExpirationInSeconds = options.SlidingExpiration?.TotalSeconds,
                Id = key,
            };

            this.cache.TryAdd(key, cacheEntry);
        }

        public Task SetAsync(string key, byte[] value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            this.Set(key, value, options);
            return Task.CompletedTask;
        }
    }

    public class CacheEntry
    {
        public string Id { get; set; }

        public byte[] Value { get; set; }

        public DateTimeOffset? ExpiresAtTime { get; set; }

        public double? SlidingExpirationInSeconds { get; set; }

        public DateTimeOffset? AbsoluteExpiration { get; set; }
    }
}
