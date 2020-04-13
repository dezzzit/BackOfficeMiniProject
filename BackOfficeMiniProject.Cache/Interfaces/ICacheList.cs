using System.Collections.Generic;

namespace BackOfficeMiniProject.Cache.Interfaces
{
    public interface ICacheList<TData> 
        where TData : class
    {
        /// <summary>
        /// Get cached value
        /// </summary>
        IEnumerable<TData> GetValues();

        /// <summary>
        /// Set data that should be cached
        /// </summary>
        void Set(IEnumerable<TData> values);

        /// <summary>
        /// Remove from cache
        /// </summary>
        void Remove();
    }
}