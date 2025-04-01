using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Core.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal PriceUnit { get; set; }
        public int MinQuantity { get; set; }
        public int SupplierId { get; set; } 
    }
}
