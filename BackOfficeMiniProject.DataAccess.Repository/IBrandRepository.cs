using System.Linq;
using BackOfficeMiniProject.DataAccess.DataModels;

namespace BackOfficeMiniProject.DataAccess.Repository
{
    /// <summary>
    ///     Brand repository.
    /// </summary>
    public interface IBrandRepository
    {
        /// <summary>
        ///     Gets all brands.
        /// </summary>
        IQueryable<Brand> Brands { get; }

        /// <summary>
        ///     Gets brand by identifier.
        /// </summary>
        /// <param name="brandId">Brand identifier.</param>
        /// <returns></returns>
        Brand Get(int brandId);

        /// <summary>
        ///     Adds or update brand.
        /// </summary>
        /// <param name="brand">The brand.</param>
        /// <returns>The number of state entries written to the database.</returns>
        int Upsert(Brand brand);

        /// <summary>
        ///     Deletes brand by brand identifier.
        /// </summary>
        /// <param name="brandId">The brand identifier.</param>
        /// <returns>The number of state entries written to the database.</returns>
        int Delete(int brandId);
    }
}