using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Core.Model
{
    public class Inventory
    {
        public int Id { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int CurrentQuantity { get; set; } // כמה יש עכשיו בפועל במכולת
        public int MinInGrocery { get; set; } // הכמות המינימלית הרצויה שהוגדרה
    }

}
