using BackOfficeMiniProject.DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeMiniProject.DataAccess.Database.Context
{
    /// <summary>
    /// Represents database session with logic of creating entities
    /// </summary>
    public class BackOfficeDbContext : DbContext
    {
        /// <summary>
        /// Provides brands returned from database
        /// </summary>
        public DbSet<Brand> Brands { get; set; }
        /// <summary>
        /// Provides orders returned from database
        /// </summary>
        public DbSet<Order> Orders { get; set; }

        /// <summary>
        /// Initialize database context
        /// </summary>
        /// <param name="options"></param>
        public BackOfficeDbContext(DbContextOptions<BackOfficeDbContext> options)
            : base(options)
        {

        }
        /// <summary>
        /// Init database structure
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasAnnotation("MySQL:AutoIncrement", true);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasAnnotation("MySQL:AutoIncrement", true);
                entity.Property(e => e.TimeReceived).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Orders);
            });

            modelBuilder.Seed();
        }
    }
}