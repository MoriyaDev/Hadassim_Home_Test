using GroceryManager.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrdersBySupplierIdAsync(int supplierId);
        Task<Order> GetOrderByIdAsync(int orderId);
        Task UpdateOrderAsync(Order order);

        Task AddOrderAsync(Order order);
        Task<List<Order>> GetAllOrdersAsync();


    }
}
