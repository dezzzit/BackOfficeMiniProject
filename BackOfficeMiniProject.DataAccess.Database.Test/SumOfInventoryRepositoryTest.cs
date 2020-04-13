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
    /// Contains tests for SumOfInventoryRepository
    /// </summary>
    public class SumOfInventoryRepositoryTest : IDisposable
    {
        private readonly IBrandRepository _brandRepository;
        private readonly ISumOfInventoryRepository _sumOfInventoryRepository;

        protected DbContextOptions<BackOfficeDbContext> DbContextOptions { get; }
        protected BackOfficeDbContext Context;

        public SumOfInventoryRepositoryTest()
        {
            DbContextOptions = new DbContextOptionsBuilder<BackOfficeDbContext>()
                .UseMySql(Settings.ConnectionString.BrandRepository)
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
