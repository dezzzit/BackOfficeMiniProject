﻿using System;
using Microsoft.EntityFrameworkCore;

namespace Users.DataAccess.Database.BaseRepositories
{
    /// <summary>
    ///     Base class for repository.
    /// </summary>
    public abstract class EntityRepository
    {
        private readonly DbContext _dbContext;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EntityRepository" /> class.
        /// </summary>
        /// <param name="dbContext">Database context.</param>
        protected EntityRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        ///     Gets database context.
        /// </summary>
        protected DbContext DbContext => _dbContext;

        /// <summary>
        ///     Gets type of database context.
        /// </summary>
        protected Type TypeDbContext => _dbContext.GetType();
    }
}