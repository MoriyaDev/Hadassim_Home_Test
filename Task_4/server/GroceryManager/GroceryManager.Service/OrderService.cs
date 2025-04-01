using GroceryManager.Core.Dtos;
using GroceryManager.Core.Model;
using GroceryManager.Core.Repositories;
using GroceryManager.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Service
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<List<Order>> GetOrdersBySupplierIdAsync(int supplierId)
        {
            return await _orderRepository.GetOrdersBySupplierIdAsync(supplierId);
        }

        public async Task<Order> ApproveOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                throw new Exception("Order not found");
            }

            order.Status = OrderStatus.InProgress;

            await _orderRepository.UpdateOrderAsync(order);

            return order;
        }

        public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
        {
            var order = new Order
            {
                SupplierId = dto.SupplierId,
                CreatedAt = dto.CreatedAt,
                Status = dto.Status,
                Items = dto.Items.Select(
                     i => new OrderItem
                     {
                         ProductId = i.ProductId,
                         Quantity = i.Quantity,
                     }).ToList()
            };

            await _orderRepository.AddOrderAsync(order);
            return order;

        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllOrdersAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await _orderRepository.GetOrderByIdAsync(orderId);
        }

        public async Task<Order> MarkOrderAsCompletedAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
                throw new Exception("Order not found");

            order.Status = OrderStatus.Completed;
            await _orderRepository.UpdateOrderAsync(order);

            return order;
        }


    }
}
