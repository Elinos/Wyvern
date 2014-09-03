using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoffeeCompany.Excel.Manager
{
    public class DiscountedReport
    {
        public string CompanyName { get; set; }

        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int NumberOfOrders { get; set; }
        public decimal TotalRevenue { get; set; }
        public decimal TotalDiscount { get; set; }
    }
}
