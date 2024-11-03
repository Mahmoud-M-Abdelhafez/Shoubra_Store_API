using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebAppStore.Models;

namespace WebAppStore.Data
{
    public class StoreContextDB : IdentityDbContext<AppUser>
    {
        public StoreContextDB(DbContextOptions<StoreContextDB> options) : base(options)
        {

        }


        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImage> ProductImages { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }

        public virtual DbSet<Cart> Carts { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=dell-Mahmoud;  Database=Store_DB;  Trusted_Connection=True; Encrypt=False;");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }

    }
}
