using BackOfficeMiniProject.DataAccess.Database.Context;
using BackOfficeMiniProject.DataAccess.Database.Repositories;
using BackOfficeMiniProject.DataAccess.DataModels;
using BackOfficeMiniProject.DataAccess.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace BackOfficeMiniProject.DataAccess.Database.Test
{
    /// <summary>
    /// Contains tests for BrandRepository
    /// </summary>
    public class BrandRepositoryTests : IDisposable
    {
        private readonly IBrandRepository _brandRepository;

        protected DbContextOptions<BackOfficeDbContext> DbContextOptions { get; }
        protected BackOfficeDbContext Context;

        public BrandRepositoryTests()
        {
            DbContextOptions = new DbContextOptionsBuilder<BackOfficeDbContext>()
                .UseMySql(Settings.ConnectionString.BrandRepository)
                .Options;

            Context = new BackOfficeDbContext(DbContextOptions);

            Context.Database.EnsureCreated();

            _brandRepository = new BrandRepository(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
        }

        [Fact]
        public void Test_Upsert_Create()
        {
            var expectedBrand = new Brand()
            {
                Id = 15,
                Name = "NewBrand"
            };

            _brandRepository.Upsert(expectedBrand);

            Brand actualBrand = _brandRepository.Get(expectedBrand.Id);

            Assert.NotNull(actualBrand);
            Assert.Equal(expectedBrand.Id, actualBrand.Id);
            Assert.Equal(expectedBrand.Name, actualBrand.Name);
        }

        [Fact]
        public void Test_Upsert_Update()
        {
            int expectedBrandId = 1;

            Brand expectedBrand = _brandRepository.Get(expectedBrandId);
            Assert.NotNull(expectedBrand);

            expectedBrand.Name = "UpdatedBrand";

            _brandRepository.Upsert(expectedBrand);

            Brand actualBrand = _brandRepository.Get(expectedBrand.Id);
            
            Assert.NotNull(actualBrand);
            Assert.Equal(expectedBrand.Id, actualBrand.Id);
            Assert.Equal(expectedBrand.Name, actualBrand.Name);
        }

        [Fact]
        public void Test_Delete()
        {
            int expectedBrandId = 1;

            Brand expectedBrand = _brandRepository.Get(expectedBrandId);
            Assert.NotNull(expectedBrand);

            _brandRepository.Delete(expectedBrandId);

            Brand actualBrand = _brandRepository.Get(expectedBrandId);
            Assert.Null(actualBrand);
        }

        
    }
}
