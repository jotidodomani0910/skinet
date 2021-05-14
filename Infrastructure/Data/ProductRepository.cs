using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _context;
        public ProductRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandAsync()
        {
            return await _context.ProductBrand.ToListAsync();
        }

        public async Task<Product> GetProductByIdAsync(int Id) => await _context.Products
        .Include(p => p.ProductBrand).Include(p => p.ProductType)
        .FirstOrDefaultAsync(s => s.Id == Id);

        public async Task<IReadOnlyList<Product>> GetProductsAsync() => await _context.Products.
        Include(p => p.ProductType).Include(p => p.ProductBrand).ToListAsync();

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductType.ToListAsync();
        }
    }
}