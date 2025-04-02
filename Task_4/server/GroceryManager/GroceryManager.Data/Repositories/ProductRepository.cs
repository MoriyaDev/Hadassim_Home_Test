using GroceryManager.Core.Model;
using GroceryManager.Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {

        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsBySupplierAsync(int supplierId)
        {
            return await _context.Products
                .Where(p => p.SupplierId == supplierId)
                .ToListAsync();
        }

        public async Task<Product?> GetByNameAndSupplierIdAsync(string productName, int supplierId)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.SupplierId == supplierId && p.Name == productName);
        }




    }
}
