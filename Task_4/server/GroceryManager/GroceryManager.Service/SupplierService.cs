using GroceryManager.Core.Dtos;
using GroceryManager.Core.Model;
using GroceryManager.Core.Repositories;
using GroceryManager.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        public async Task<Supplier> RegisterSupplierAsync(SupplierRegisterDto dto)
        {
            var supplier = new Supplier
            {
                CompanyName = dto.CompanyName,
                PhoneNumber = dto.PhoneNumber,
                AgentName = dto.AgentName,
                Password = HashPassword(dto.Password), // כאן!!!
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

            if (supplier == null)
                throw new Exception("UserNotFound");

            var hashedInput = HashPassword(dto.Password);

            if (supplier.Password != hashedInput)
                throw new Exception("WrongPassword");

            return supplier;
        }



    }
}
