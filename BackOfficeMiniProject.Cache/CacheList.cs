using System;
using System.Collections.Generic;
using System.Linq;
using BackOfficeMiniProject.Cache.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace BackOfficeMiniProject.Cache
{
    /// <summary>
    /// Provides caching logic for list of entities
    /// </summary>
    /// <typeparam name="TData">Entity</typeparam>
    public class CacheList<TData> : ICacheList<TData>
        where TData : class
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IEnumerable<TData> _dataList;
        private readonly string _key;
        private readonly int _expireMinutes;

        /// <summary>
        /// Initialize cache
        /// </summary>
        public CacheList(
            IMemoryCache memoryCache,
            IEnumerable<TData> dataList,
            string key = nameof(ICacheList<TData>),
            int expireMinutes = 10)
        {
            _memoryCache = memoryCache;
            _dataList = dataList;
            _key = key;
            _expireMinutes = expireMinutes;
        }

        /// <inheritdoc />
        public IEnumerable<TData> GetValues()
        {
            if (!_memoryCache.TryGetValue(_key, out IEnumerable<TData> values))
            {
                if (_dataList != null)
                {
                    values = _dataList;
                    Set(values);
                }
            }

            return values;
        }

        /// <inheritdoc />
        public void Set(IEnumerable<TData> values)
        {
            _memoryCache.Set(_key, values, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_expireMinutes)
            });
        }

        /// <inheritdoc />
        public void Remove()
        {
            if (_memoryCache.TryGetValue(_key, out IEnumerable<TData> values))
            {
                _memoryCache.Remove(_key);
            }
        }
    }
}