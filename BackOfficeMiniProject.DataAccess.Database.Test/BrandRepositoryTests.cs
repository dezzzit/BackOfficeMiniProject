using BackOfficeMiniProject.DataAccess.Database.Context;
using BackOfficeMiniProject.DataAccess.Database.Repositories;
using BackOfficeMiniProject.DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace BackOfficeMiniProject.DataAccess.Database.Test
{
    public class BrandRepositoryTests : IDisposable
    {
        private readonly BrandRepository _brandRepository;

        protected DbContextOptions<BackOfficeDbContext> dbContextOptions { get; }
        protected BackOfficeDbContext context;
        protected string connectionString = "Server=localhost;port=3306;Database=test10;User=root;Password=12345;";

        public BrandRepositoryTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<BackOfficeDbContext>()
                .UseMySql(connectionString)
                .Options;

            context = new BackOfficeDbContext(dbContextOptions);

            context.Database.EnsureCreated();

            _brandRepository = new BrandRepository(context);
        }

        public void Dispose()
        {
            context.Database.EnsureDeleted();
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
