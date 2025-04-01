using GroceryManager.Core.Model;
using GroceryManager.Core.Repositories;
using GroceryManager.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Service
{
    public class ProductService: IProductService
    {
        private readonly IProductRepository _productRepository;
        private List<Product> propa;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public List<Product> GetAll()
        {
            propa = new List<Product>();
            return propa;
        }

        public async Task<List<Product>> GetProductsBySupplierAsync(int supplierId)
        {
            return await _productRepository.GetProductsBySupplierAsync(supplierId);
        }

    }
}
