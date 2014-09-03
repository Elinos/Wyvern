using System;
using System.Linq;

namespace CoffeeCompany.SQLite.Models
{
    public class Discount
    {
        public int DiscountId { get; set; }
        public int CompanyId { get; set; }  
        public int DiscountTypeID { get; set; }
        public DiscountType DiscountType { get; set; }
    }
}
