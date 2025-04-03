using GroceryManager.Core.Dtos;
using GroceryManager.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Core.Repositories
{
    public interface ISupplierRepository
    {
        Task AddSupplierAsync(Supplier supplier);
        Task<Supplier?> GetByNameAsync(string name);
        Task<BestSupplier?> GetCheapestSupplierForProductAsync(string productName);
    }
}
