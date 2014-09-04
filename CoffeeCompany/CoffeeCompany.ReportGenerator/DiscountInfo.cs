using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCompany.ReportGenerator
{
    public class DiscountInfo
    {
        public int CompanyId { get; set; }

        public decimal TotalSpending { get; set; }

        public DiscountType Type 
        { 
            get 
            {
                if (this.TotalSpending >= 0 && this.TotalSpending < 5000)
                {
                    return DiscountType.Regular;
                }
                else if (this.TotalSpending >= 5000 && this.TotalSpending < 20000)
                {
                    return DiscountType.Silver;
                }
                else if (this.TotalSpending >= 20000 && this.TotalSpending < 25000)
                {
                    return DiscountType.Gold;
                }
                else
                {
                    return DiscountType.Platium;
                }
            } 
        }
    }
}
