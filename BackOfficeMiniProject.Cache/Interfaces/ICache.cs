namespace BackOfficeMiniProject.Cache.Interfaces
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TData"></typeparam>
    public interface ICache<TData> 
        where TData : class
    {
        /// <summary>
        /// Get cached value
        /// </summary>
        TData GetValue();

        /// <summary>
        /// Set data that should be cached
        /// </summary>
        void Set(TData value);

        /// <summary>
        /// Remove from cache
        /// </summary>
        void Remove();
    }
}