using GroceryManager.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Core.Repositories
{
    public interface IInventoryRepository
    {
        Task<Inventory> GetByNameAsync(string productName);
        Task UpdateAsync(Inventory item);
    }
}
