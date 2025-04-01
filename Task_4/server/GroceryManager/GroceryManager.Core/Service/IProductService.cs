using GroceryManager.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Core.Service
{
    public interface IProductService
    {

        Task<List<Product>> GetProductsBySupplierAsync(int supplierId);

    }
}
