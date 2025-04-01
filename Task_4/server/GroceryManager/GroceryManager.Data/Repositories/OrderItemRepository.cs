using GroceryManager.Core.Model;
using GroceryManager.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Data.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {

        private readonly DataContext _context;


        public OrderItemRepository(DataContext context)
        {
            _context = context;
        }
       
    }
}
