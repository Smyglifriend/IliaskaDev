using IliaskaWebSite.EfStuff.Model;
using Microsoft.EntityFrameworkCore;

namespace IliaskaWebSite.EfStuff
{
    public class IliaskaDbContext : DbContext
    {
        public IliaskaDbContext(DbContextOptions options) : base(options) { }
        
        public DbSet<User> Users { get; set; }
        
        public DbSet<Product> Products { get; set; }
        
        public DbSet<ProductCategories> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductCategories>()
                .HasMany(x => x.Clothes)
                .WithOne(x => x.Category);
            
            base.OnModelCreating(modelBuilder);
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}   