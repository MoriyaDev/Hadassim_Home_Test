using GroceryManager.Core.Dtos;
using GroceryManager.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Core.Repositories
{
    public interface ISupplierService
    {
        Task<Supplier> RegisterSupplierAsync(SupplierRegisterDto dto);
        Task<Supplier?> LoginAsync(SupplierLoginDto dto);
        List<Supplier> GetAllSuppliers();


    }
}
