using Microsoft.EntityFrameworkCore;
using Shopii.App.Application.Database;
using Shopii.App.Application.Models;

namespace Shopii.App.Application.Services
{
    public class ProductService
    {
        private readonly IDbContextFactory<ShopiDbContext> _factory;

        public ProductService(IDbContextFactory<ShopiDbContext> factory)
        {
            _factory = factory;
        }

        public async Task<Product?> FindProductAsync(int productId)
        {
            using var context = _factory.CreateDbContext();
            return await context.Products.FindAsync(productId);
        }

        public async Task<Product?> FindProductAsync(string productName)
        {
            using var context = _factory.CreateDbContext();
            return await context.Products.FirstOrDefaultAsync(x => x.Name == productName);
        }

        public async Task<Product> CreateProductAsync(Product product)
        {
            using var context = _factory.CreateDbContext();
            var addedProduct = await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return addedProduct.Entity;
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            using var context = _factory.CreateDbContext();
            return await context.Products
                    .ToListAsync();
        }
    }
}
