using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Core.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<OrderItem> Items { get; set; } = new();
        public OrderStatus Status { get; set; } = OrderStatus.Pending; // ברירת מחדל - ממתינה
    }

}
