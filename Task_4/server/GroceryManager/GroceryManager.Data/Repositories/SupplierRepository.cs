using GroceryManager.Core.Dtos;
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
    public class SupplierRepository : ISupplierRepository
    {

        private readonly DataContext _context;

        public List<Supplier> GetAllSuppliers()
        {
            return _context.Suppliers.ToList();
        }
        public SupplierRepository(DataContext context)
        {
            _context = context;
        }
        public async Task AddSupplierAsync(Supplier supplier)
        {
            _context.Suppliers.Add(supplier);
            await _context.SaveChangesAsync();
        }

        public async Task<Supplier?> GetByNameAsync(string name)
        {
            return await _context.Suppliers
                .FirstOrDefaultAsync(s => s.CompanyName == name);
        }


        public async Task<BestSupplier?> GetCheapestSupplierForProductAsync(string productName)
        {
            return await _context.Products
                .Where(p => p.Name == productName)
                .OrderBy(p => p.PriceUnit)
                .Select(p => new BestSupplier
                {
                    SupplierId = p.SupplierId,
                    ProductId = p.Id,
                    MinQuantity = p.MinQuantity
                })
                .FirstOrDefaultAsync();
        }


    }
}