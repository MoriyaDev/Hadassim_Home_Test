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
    public class InventoryService : IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IOrderRepository _orderRepository;

        public InventoryService(
            IInventoryRepository inventoryRepository,
            ISupplierRepository supplierRepo,
            IOrderRepository orderRepo)
        {
            _inventoryRepository = inventoryRepository;
            _supplierRepository = supplierRepo;
            _orderRepository = orderRepo;
        }
        public async Task<List<Inventory>> GetAllAsync()
        {
            return await _inventoryRepository.GetAllAsync();
        }
        public async Task<List<string>> HandleSaleAndCheckStockAsync(Dictionary<string, int> soldItems)
        {
            var missingProducts = new List<string>();

            foreach (var item in soldItems)
            {
                var productName = item.Key;
                var quantitySold = item.Value;

                var inventoryItem = await _inventoryRepository.GetByNameAsync(productName);
                if (inventoryItem == null)
                {
                    missingProducts.Add($"{productName} - המוצר לא נמצא במלאי");
                    continue;
                }

                inventoryItem.CurrentQuantity -= quantitySold;

                if (inventoryItem.CurrentQuantity < inventoryItem.MinInGrocery)
                {
                    var bestSupplier = await _supplierRepository.GetCheapestSupplierForProductAsync(productName);

                    if (bestSupplier != null)
                    {
                        var order = new Order
                        {
                            SupplierId = bestSupplier.SupplierId,
                            CreatedAt = DateTime.Now,
                            Status = OrderStatus.Pending,
                            Items = new List<OrderItem>
                            {
                                new OrderItem
                                {
                                    ProductId = bestSupplier.ProductId,
                                    Quantity = bestSupplier.MinQuantity
                                }
                            }
                        };

                        await _orderRepository.AddOrderAsync(order);
                    }
                    else
                    {
                        missingProducts.Add($"{productName} - אף ספק אינו משווק את המוצר");
                    }
                }

                await _inventoryRepository.UpdateAsync(inventoryItem);
            }

            return missingProducts;
        }
    }
}
