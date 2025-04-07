using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GroceryManager.Core.Dtos;

namespace GroceryManager.Core.Dtos
{
    public class SupplierRegisterDto
    {
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string AgentName { get; set; }
        public string Password { get; set; } 
        public List<ProductDto> Products { get; set; }
    }
}
