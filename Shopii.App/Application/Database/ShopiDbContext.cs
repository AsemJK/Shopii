using Microsoft.EntityFrameworkCore;
using Shopii.App.Application.Models;

namespace Shopii.App.Application.Database
{
    public class ShopiDbContext : DbContext
    {
        public ShopiDbContext(DbContextOptions<ShopiDbContext> options) : base(options)
        { }

        public virtual DbSet<User> Users { set; get; }
        public virtual DbSet<Role> Roles { set; get; }
        public virtual DbSet<UserRole> UserRoles { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<CartItem> CartItems { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            // it should be placed here, otherwise it will rewrite the following settings!
            base.OnModelCreating(builder);

            // Custom application mappings
            builder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).HasMaxLength(450).IsRequired();
                entity.HasIndex(e => e.Email).IsUnique();
                entity.Property(e => e.Password).IsRequired();
            });

            builder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(450).IsRequired();
                entity.HasIndex(e => e.Name).IsUnique();
            });

            builder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
                entity.HasIndex(e => e.UserId);
                entity.HasIndex(e => e.RoleId);
                entity.Property(e => e.UserId);
                entity.Property(e => e.RoleId);
                entity.HasOne(d => d.Role).WithMany(p => p.UserRoles).HasForeignKey(d => d.RoleId);
                entity.HasOne(d => d.User).WithMany(p => p.UserRoles).HasForeignKey(d => d.UserId);
            });

            builder.Entity<Role>().HasData(
                new Role { Id = 1, Name = CustomRoles.User },
                new Role { Id = 2, Name = CustomRoles.Admin }
            );

            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Electronics" },
                new Category { Id = 2, Name = "Clothing" },
                new Category { Id = 3, Name = "Books" }
            );

            builder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(450).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(450).IsRequired();
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.Stock).IsRequired();
                entity.Property(e => e.CategoryId).IsRequired().HasDefaultValue(1);
                entity.HasOne(d => d.Category).WithMany(p => p.Products).HasForeignKey(d => d.CategoryId);
            });

            builder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Laptop", Description = "A laptop", Price = 1000, Stock = 10, CategoryId = 1 },
                new Product { Id = 2, Name = "T-shirt", Description = "A t-shirt", Price = 20, Stock = 100, CategoryId = 2 },
                new Product { Id = 3, Name = "Book", Description = "A book", Price = 10, Stock = 50, CategoryId = 3 }
            );
        }
    }
}
