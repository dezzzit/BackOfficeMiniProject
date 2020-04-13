using System.Collections.Generic;
using System.Linq;
using BackOfficeMiniProject.DataAccess.DataModels;

namespace BackOfficeMiniProject.DataAccess.Repository
{
    /// <summary>
    ///  Brand repository interface describes CRUD operations for brand
    /// </summary>
    public interface IBrandRepository
    {
        /// <summary>
        ///     Gets all brands.
        /// </summary>
        IEnumerable<Brand> Brands { get; }

        /// <summary>
        /// Provide brand by id 
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns>Brand's entity</returns>
        Brand Get(int brandId);

        /// <summary>
        /// Update or insert new brand, depend on id existing
        /// </summary>
        /// <param name="brand">Brand item</param>
        /// <returns>New or updated id</returns>
        int Upsert(Brand brand);

        /// <summary>
        /// Deletes brand item by id
        /// </summary>
        /// <param name="brandId">brand's id</param>
        /// <returns>deleted items count</returns>
        int Delete(int brandId);
    }
}