namespace CoffeeCompany.ReportGenerator
{
    public class OrderInfo
    {
       
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; }
        public decimal RevenueFromOrder { get; set; }
    }
}
