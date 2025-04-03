using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Core.Dtos
{
    public class BestSupplier
    {
        public int SupplierId { get; set; }
        public int ProductId { get; set; }
        public int MinQuantity { get; set; }
    }
}
