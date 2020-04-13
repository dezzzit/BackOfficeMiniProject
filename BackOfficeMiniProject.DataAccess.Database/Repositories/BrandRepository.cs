using System.Collections.Generic;
using System.Linq;
using BackOfficeMiniProject.DataAccess.Database.BaseRepositories;
using BackOfficeMiniProject.DataAccess.Database.Context;
using BackOfficeMiniProject.DataAccess.DataModels;
using BackOfficeMiniProject.DataAccess.Repository;

namespace BackOfficeMiniProject.DataAccess.Database.Repositories
{
    /// <summary>
    /// Contains logic of CRUD operations for the brand entity.
    /// </summary>
    public class BrandRepository : ContextEntityRepository, IBrandRepository
    {
        /// <summary>
        ///Initializes a new instance of the <see cref="BrandRepository" /> class.
        /// </summary>
        /// <param name="context">Database user context.</param>
        public BrandRepository(BackOfficeDbContext context)
            : base(context)
        {

        }

        /// <summary>
        /// Provide all brands list
        /// </summary>
        public IEnumerable<Brand> Brands => Context.Brands;

        /// <summary>
        /// Provide brand by id 
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns>Brand's entity</returns>
        public Brand Get(int brandId)
        {
            return Context.Brands
                .FirstOrDefault(brand => brand.Id == brandId);
        }

        /// <summary>
        /// Update or insert new brand, depend on id existing
        /// </summary>
        /// <param name="brand">Brand item</param>
        /// <returns>New or updated id</returns>
        public int Upsert(Brand brand)
        {
            if (Get(brand.Id) == null)
            {
                Context.Brands
                    .Add(brand);
            }
            else
            {
                Context.Brands
                    .Update(brand);
            }

            return Context.SaveChanges();
        }

        /// <summary>
        /// Deletes brand item by id
        /// </summary>
        /// <param name="brandId">brand's id</param>
        /// <returns>deleted items count</returns>
        public int Delete(int brandId)
        {
            Context.Brands
                .Remove(Get(brandId));

            return Context.SaveChanges();
        }
    }
}
