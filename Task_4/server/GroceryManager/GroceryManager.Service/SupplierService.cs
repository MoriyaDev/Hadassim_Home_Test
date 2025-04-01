using GroceryManager.Core.Dtos;
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
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;

        public SupplierService(ISupplierRepository supplierRepository)
        {
            _supplierRepository = supplierRepository;
        }
        public async Task<Supplier> RegisterSupplierAsync(SupplierRegisterDto dto)
        {
            var supplier = new Supplier
            {
                CompanyName = dto.CompanyName,
                PhoneNumber = dto.PhoneNumber,
                AgentName = dto.AgentName,
                Password = dto.Password, // כאן!!!
                Products = dto.Products.Select(p => new Product
                {
                    Name = p.Name,
                    PriceUnit = p.PriceUnit,
                    MinQuantity = p.MinQuantity
                }).ToList()
            };

            await _supplierRepository.AddSupplierAsync(supplier);
            return supplier;
        }

        public async Task<Supplier?> LoginAsync(SupplierLoginDto dto)
        {
            var supplier = await _supplierRepository.GetByNameAsync(dto.CompanyName);

            if (supplier == null || supplier.Password != dto.Password)
                return null;

            return supplier;
        }



    }
}
