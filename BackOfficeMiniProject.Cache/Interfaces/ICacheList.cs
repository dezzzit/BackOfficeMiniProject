using System.Collections.Generic;

namespace BackOfficeMiniProject.Cache.Interfaces
{
    public interface ICacheList<TData> 
        where TData : class
    {
        IEnumerable<TData> GetValues();

        void Set(IEnumerable<TData> values);

        void Remove();
    }
}