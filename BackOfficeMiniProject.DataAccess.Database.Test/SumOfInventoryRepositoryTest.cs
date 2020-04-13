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

        protected DbContextOptions<BackOfficeDbContext> dbContextOptions { get; }
        protected BackOfficeDbContext context;

        public SumOfInventoryRepositoryTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<BackOfficeDbContext>()
                .UseMySql(Settings.ConnectionString.SumOfInventory)
                .Options;

            context = new BackOfficeDbContext(dbContextOptions);

            context.Database.EnsureCreated();

            _brandRepository = new BrandRepository(context);
            _sumOfInventoryRepository = new SumOfInventoryRepository(context);
        }
        
        public void Dispose()
        {
            context.Database.EnsureDeleted();
        }

        [Fact]
        public void Test_SumOfInventory()
        {
            int expectedBrandCount = context.Brands
                .Count();

            int[] expectedBrandIds = context.Brands
                .Select(b => b.Id)
                .ToArray();

            List<SumOfInventory> actualSumOfInventories = _sumOfInventoryRepository.SumOfInventory
                .ToList();

            foreach (var expectedBrandId in expectedBrandIds)
            {
                Brand expectedBrand = _brandRepository.Get(expectedBrandId);
                Assert.NotNull(expectedBrand);

                int expectedSum = context.Orders
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
