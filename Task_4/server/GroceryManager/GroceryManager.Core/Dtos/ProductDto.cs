using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Core.Dtos
{
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal PriceUnit { get; set; }
        public int MinQuantity { get; set; }
    }
}
