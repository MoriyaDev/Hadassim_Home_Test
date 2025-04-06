using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Core.Model
{
    public class Supplier
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string PhoneNumber { get; set; }
        public string AgentName { get; set; }
        public List<Product> Products { get; set; }
        public string Password { get; set; }

        public string Role { get; set; } = "Supp";

    }
}
