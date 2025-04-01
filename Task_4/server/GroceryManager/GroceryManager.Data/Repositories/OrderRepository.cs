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
    public class OrderRepository : IOrderRepository
    {

        private readonly DataContext _context;


        public OrderRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<List<Order>> GetOrdersBySupplierIdAsync(int supplierId)
        {
            return await _context.Orders
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .Where(o => o.SupplierId == supplierId)
                .ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.Supplier)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }



        public async Task UpdateOrderAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Supplier)
                .Include(o => o.Items)
                    .ThenInclude(i => i.Product)
                .ToListAsync();
        }

     



    }
}
