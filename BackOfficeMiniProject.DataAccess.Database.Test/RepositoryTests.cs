using BackOfficeMiniProject.DataAccess.Database.Context;
using BackOfficeMiniProject.DataAccess.Database.Repositories;
using BackOfficeMiniProject.DataAccess.DataModels;
using BackOfficeMiniProject.DataAccess.Repository;
using BackOfficeMiniProject.Reports.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BackOfficeMiniProject.DataAccess.Database.Test
{
    /// <summary>
    /// Contains tests for BrandRepository and SumOfInventoryRepository
    /// </summary>
    public class RepositoryTests : IDisposable
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ISumOfInventoryRepository _sumOfInventoryRepository;

        protected DbContextOptions<BackOfficeDbContext> DbContextOptions { get; }
        protected BackOfficeDbContext Context;

        public RepositoryTests()
        {
            DbContextOptions = new DbContextOptionsBuilder<BackOfficeDbContext>()
                .UseMySql(Settings.ConnectionString.TestProjectConnectionString)
                .Options;

            Context = new BackOfficeDbContext(DbContextOptions);

            Context.Database.EnsureCreated();

            _brandRepository = new BrandRepository(Context);
            _sumOfInventoryRepository = new SumOfInventoryRepository(Context);
        }

        public void Dispose()
        {
            Context.Database.EnsureDeleted();
        }

        [Fact]
        public void Test_BrandRepository_Upsert_Create()
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
        public void Test_BrandRepository_Upsert_Update()
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
        public void Test_BrandRepository_Delete()
        {
            int expectedBrandId = 1;

            Brand expectedBrand = _brandRepository.Get(expectedBrandId);
            Assert.NotNull(expectedBrand);

            _brandRepository.Delete(expectedBrandId);

            Brand actualBrand = _brandRepository.Get(expectedBrandId);
            Assert.Null(actualBrand);
        }

        [Fact]
        public void Test_SumOfInventory()
        {
            int expectedBrandCount = Context.Brands
                .Count();

            int[] expectedBrandIds = Context.Brands
                .Select(b => b.Id)
                .ToArray();

            List<SumOfInventory> actualSumOfInventories = _sumOfInventoryRepository.SumOfInventory
                .ToList();

            foreach (var expectedBrandId in expectedBrandIds)
            {
                Brand expectedBrand = _brandRepository.Get(expectedBrandId);
                Assert.NotNull(expectedBrand);

                int expectedSum = Context.Orders
                    .Where(o => o.BrandId == expectedBrand.Id)
                    .Select(s => s.Quantity)
                    .Sum();

                SumOfInventory actualSumOfInventory = actualSumOfInventories
                    .FirstOrDefault(i => i.BrandName.Equals(expectedBrand.Name));

                Assert.Equal(expectedBrand.Name, actualSumOfInventory.BrandName);
                Assert.Equal(expectedSum, actualSumOfInventory.Quantity);
            }

            Assert.Equal(expectedBrandCount, actualSumOfInventories.Count);
        }
    }
}
