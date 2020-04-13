using System;
using BackOfficeMiniProject.DataAccess.Database.Context;

namespace BackOfficeMiniProject.DataAccess.Database.BaseRepositories
{
    /// <summary>
    ///     Base class for repository of backOffice context.
    /// </summary>
    public abstract class ContextEntityRepository : EntityRepository
    {
        private readonly BackOfficeDbContext _context;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ContextEntityRepository" /> class.
        /// </summary>
        /// <param name="dbContext">Database backOffice context.</param>
        protected ContextEntityRepository(BackOfficeDbContext dbContext)
            : base(dbContext)
        {
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        /// <summary>
        ///     Gets database backOffice context.
        /// </summary>
        protected BackOfficeDbContext Context => _context;

        /// <summary>
        ///     Gets type of database backOffice context.
        /// </summary>
        protected Type TypeUserContext => _context.GetType();
    }
}