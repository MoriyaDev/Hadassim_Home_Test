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
    public class OrderItemService : IOrderItemService
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemService(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository;
        }
      

    }
}
