using GroceryManager.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Core.Dtos
{
    public class CreateOrderDto
    {
        public string SupplierName { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<CreateOrderItemDto> Items { get; set; }
    }
}
