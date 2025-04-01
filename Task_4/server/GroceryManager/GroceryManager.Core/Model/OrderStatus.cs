using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroceryManager.Core.Model
{
    public enum OrderStatus
    {
        Pending,     // ממתינה לאישור ספק
        InProgress,  // הספק אישר – הזמנה בתהליך
        Completed    // הושלמה על ידי בעל המכולת
    }

}
