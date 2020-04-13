namespace BackOfficeMiniProject.Cache.AppSettings
{
    /// <summary>
    /// Provide settings for caching
    /// </summary>
    public class CacheSetting
    {
        /// <summary>
        /// Life period of stored data
        /// </summary>
        public int ExpireMinutes { get; set; }
    }
}