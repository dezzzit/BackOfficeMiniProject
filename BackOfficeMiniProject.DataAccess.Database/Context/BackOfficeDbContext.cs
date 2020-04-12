using BackOfficeMiniProject.DataAccess.DataModels;
using Microsoft.EntityFrameworkCore;

namespace BackOfficeMiniProject.DataAccess.Database.Context
{
    public class BackOfficeDbContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Order> Orders { get; set; }

        public BackOfficeDbContext(DbContextOptions<BackOfficeDbContext> options)
            : base(options)
        {

        }

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