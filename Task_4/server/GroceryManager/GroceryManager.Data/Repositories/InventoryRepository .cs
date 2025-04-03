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
    public class InventoryRepository :IInventoryRepository
    {
        private readonly DataContext _context;


        public InventoryRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Inventory?> GetByNameAsync(string productName)
        {
            return await _context.Inventorys
                .FirstOrDefaultAsync(p => p.ProductName == productName);
        }

        public async Task UpdateAsync(Inventory item)
        {
            _context.Inventorys.Update(item);
            await _context.SaveChangesAsync();
        }

    }
}
