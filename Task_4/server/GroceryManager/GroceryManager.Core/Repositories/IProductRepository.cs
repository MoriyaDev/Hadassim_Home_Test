using GroceryManager.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Core.Repositories
{
    public interface IProductRepository
    {
        Task<Product?> GetByNameAndSupplierIdAsync(string productName, int supplierId);
        Task<List<Product>> GetProductsBySupplierAsync(int supplierId);

    }
}
