using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Core.Dtos
{
    public class SaleUpdateDto
    {
        public Dictionary<string, int> SoldItems { get; set; } = new();

    }
}
