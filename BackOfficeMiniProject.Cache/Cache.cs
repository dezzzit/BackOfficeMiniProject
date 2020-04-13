using System;
using BackOfficeMiniProject.Cache.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace BackOfficeMiniProject.Cache
{
    /// <summary>
    /// Provides caching logic for entities
    /// </summary>
    /// <typeparam name="TData">Entity</typeparam>
    public class Cache<TData> : ICache<TData>
        where TData : class
    {
        private readonly IMemoryCache _memoryCache;
        private readonly TData _data;
        private readonly string _key;
        private readonly int _expireMinutes;

        /// <summary>
        /// Initialize cache
        /// </summary>
        public Cache(
            IMemoryCache memoryCache,
            TData data,
            string key = nameof(ICache<TData>),
            int expireMinutes = 10)
        {
            _memoryCache = memoryCache;
            _data = data;
            _key = key;
            _expireMinutes = expireMinutes;
        }

        /// <inheritdoc />
        public TData GetValue()
        {
            if (!_memoryCache.TryGetValue(_key, out TData value))
            {
                if (_data != null)
                {
                    value = _data;
                    Set(value);
                }
            }

            return value;
        }

        /// <inheritdoc />
        public void Set(TData value)
        {
            _memoryCache.Set(_key, value, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_expireMinutes)
            });
        }

        /// <inheritdoc />
        public void Remove()
        {
            if (_memoryCache.TryGetValue(_key, out TData value))
            {
                _memoryCache.Remove(_key);
            }
        }
    }
}