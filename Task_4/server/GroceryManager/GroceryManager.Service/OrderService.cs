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
        private readonly ISupplierRepository _supplierRepository;
        private readonly IProductRepository _productRepository;
        private readonly IInventoryRepository _inventoryRepository;


        public OrderService(
            IOrderRepository orderRepository,
            ISupplierRepository supplierRepository
           , IProductRepository productRepository,
            IInventoryRepository inventoryRepository)
        {
            _orderRepository = orderRepository;
            _supplierRepository = supplierRepository;
            _productRepository = productRepository;
            _inventoryRepository = inventoryRepository;
        }

        public async Task<List<Order>> GetOrdersBySupplierIdAsync(int supplierId)
        {
            return await _orderRepository.GetOrdersBySupplierIdAsync(supplierId);
        }

        public async Task<Order> ApproveOrderAsync(int orderId)
        {
            var order = await _orderRepository.GetOrderByIdAsync(orderId);


            order.Status = OrderStatus.InProgress;

            await _orderRepository.UpdateOrderAsync(order);

            return order;
        }

        public async Task<Order> CreateOrderAsync(CreateOrderDto dto)
        {
            var supplier = await _supplierRepository.GetByNameAsync(dto.SupplierName);
            if (supplier == null)
                throw new Exception("אין ספק כזה בDB");

            var order = new Order
            {
                SupplierId = supplier.Id,
                CreatedAt = dto.CreatedAt,
                Status = OrderStatus.Pending,
                Items = new List<OrderItem>()
            };

            foreach (var item in dto.Items)
            {
                var product = await _productRepository.GetByNameAndSupplierIdAsync(item.ProductName, supplier.Id);
                if (product == null)
                    throw new Exception($"Product '{item.ProductName}' not found for supplier ");
                if (item.Quantity < product.MinQuantity)
                    throw new Exception($"לא ניתן להזמין את '{product.Name}' בכמות קטנה מ-{product.MinQuantity}");
                order.Items.Add(new OrderItem
                {
                    ProductId = product.Id,
                    Quantity = item.Quantity
                });
            }

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
            if (order.Status != OrderStatus.InProgress)
                throw new Exception("הספק עדיין לא אישר את ההזמנה");

            if (order == null)
            {
                throw new Exception("Order not found");
            }
            foreach (var item in order.Items)
            {
                if (_inventoryRepository == null)
                    throw new Exception("_inventoryRepository is null");

                if (item == null)
                    throw new Exception("item is null");

                if (item.ProductId == null)
                    throw new Exception("item.ProductId is null");

                var existingProduct = await _inventoryRepository.GetByProductIdAsync(item.ProductId.ToString());

                if (existingProduct != null)
                {
                    existingProduct.CurrentQuantity += item.Quantity;
                    await _inventoryRepository.UpdateAsync(existingProduct);
                }
                else
                {
                    var newProduct = new Inventory
                    {
                        ProductId = item.ProductId.ToString(),
                        ProductName = item.Product.Name,
                        CurrentQuantity = item.Quantity,
                        MinInGrocery = 2
                    };


                    await _inventoryRepository.AddAsync(newProduct);
                }
            }
            order.Status = OrderStatus.Completed;

            await _orderRepository.UpdateOrderAsync(order);

            return order;
        }


    }
}
