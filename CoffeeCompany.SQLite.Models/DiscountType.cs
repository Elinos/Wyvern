using System;
using System.Linq;

namespace CoffeeCompany.SQLite.Models
{
    public class DiscountType
    {
        public int DiscountTypeID { get; set; }
        public string Name { get; set; }
        public int DiscountPercent { get; set; }
    }
}
