using System.Linq;
using BackOfficeMiniProject.DataAccess.Database.BaseRepositories;
using BackOfficeMiniProject.DataAccess.Database.Context;
using BackOfficeMiniProject.DataAccess.DataModels;
using BackOfficeMiniProject.DataAccess.Repository;

namespace BackOfficeMiniProject.DataAccess.Database.Repositories
{
    public class BrandRepository : ContextEntityRepository, IBrandRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BrandRepository" /> class.
        /// </summary>
        /// <param name="context">Database user context.</param>
        public BrandRepository(BackOfficeDbContext context)
            : base(context)
        {

        }
        public IQueryable<Brand> Brands => Context.Brands;
        public Brand Get(int brandId)
        {
            return Context.Brands
                .FirstOrDefault(brand => brand.Id == brandId);
        }
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
        public int Delete(int brandId)
        {
            Context.Brands
                .Remove(Get(brandId));

            return Context.SaveChanges();
        }
    }
}
