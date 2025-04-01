using GroceryManager.Core.Dtos;
using GroceryManager.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Core.Repositories
{
    public interface IOrderService
    {
        Task<List<Order>> GetOrdersBySupplierIdAsync(int supplierId);
        Task<Order> ApproveOrderAsync(int orderId);
        Task<Order> CreateOrderAsync(CreateOrderDto dto);
        Task<List<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int orderId);
        Task<Order> MarkOrderAsCompletedAsync(int orderId);

    }
}
