using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ProductManagement.Core.Domain.Entities;

namespace ProductManagement.Infrastructure.Persistence.Contexts
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Business> Business { get; set; }

        /* 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(projectPath)
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
        */
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            #region Tables

            modelBuilder.Entity<Business>(entity =>
            {
                entity.ToTable("Business");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired();
                entity.Property(p => p.IdUser).IsRequired();
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired();
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Products");
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name).IsRequired();
            });

            #endregion

            #region Relationships

            modelBuilder.Entity<Business>(entity =>
            {
                entity.HasMany(p => p.Categories)
               .WithOne(p => p.Business)
               .HasForeignKey(p => p.BusinessId)
               .OnDelete(DeleteBehavior.Cascade);
            });
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasMany(p => p.Products)
               .WithOne(p => p.Category)
               .HasForeignKey(p => p.CategoryId)
               .OnDelete(DeleteBehavior.Cascade);
            });

            #endregion
        }
    }
}
