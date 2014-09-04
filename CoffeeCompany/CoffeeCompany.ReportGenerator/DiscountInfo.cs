using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoffeeCompany.ReportGenerator
{
    public class DiscountInfo
    {
        private const int RegularDiscountID = 1;
        private const int SilverDiscountID = 2;
        private const int GoldDiscountID = 3;
        private const int PlatinumDiscountID = 4;

        public int CompanyId { get; set; }

        public decimal TotalSpending { get; set; }

        public int TypeID 
        { 
            get 
            {
                if (this.TotalSpending >= 0 && this.TotalSpending < 5000)
                {
                    return RegularDiscountID;
                }
                else if (this.TotalSpending >= 5000 && this.TotalSpending < 20000)
                {
                    return SilverDiscountID;
                }
                else if (this.TotalSpending >= 20000 && this.TotalSpending < 25000)
                {
                    return GoldDiscountID;
                }
                else
                {
                    return PlatinumDiscountID;
                }
            } 
        }
    }
}
