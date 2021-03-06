﻿using System.Collections.Generic;
using System.Linq;
using BackOfficeMiniProject.DataAccess.Database.BaseRepositories;
using BackOfficeMiniProject.DataAccess.Database.Context;
using BackOfficeMiniProject.DataAccess.Repository;
using BackOfficeMiniProject.Reports.Models;

namespace BackOfficeMiniProject.DataAccess.Database.Repositories
{
    public class SumOfInventoryRepository : ContextEntityRepository, ISumOfInventoryRepository
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BrandRepository" /> class.
        /// </summary>
        /// <param name="context">Database user context.</param>
        public SumOfInventoryRepository(BackOfficeDbContext context)
            : base(context)
        {

        }

        /// <inheritdoc />
        public IEnumerable<SumOfInventory> SumOfInventory => Context.Orders
            .Join(Context.Brands, o => o.BrandId, b => b.Id, (o, b) => new { o, b })
            .GroupBy(x => x.b.Name)
            .Select(g => new SumOfInventory(g.Sum(item => item.o.Quantity), g.Key));
    }
}
