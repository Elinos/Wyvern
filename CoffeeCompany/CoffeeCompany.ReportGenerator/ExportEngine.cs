namespace CoffeeCompany.ReportGenerator
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Collections;

    using CoffeeCompany.Data;
    using CoffeeCompany.Models;

    public class ReportsEngine
    {
        public ReportsEngine()
            : this (new CoffeeCompanyData())
        {
            this.ExportPdf = new PDFExporter();
            this.ExportXml = new XMLExporter();
            this.ExportJson = new JsonExporter();
        }
        public ReportsEngine(ICoffeeCompanyData data)
        {
            this.ExportPdf = new PDFExporter();
            this.ExportXml = new XMLExporter();
            this.ExportJson = new JsonExporter();
            this.Data = data;
        }
        public PDFExporter ExportPdf { get; set; }

        public XMLExporter ExportXml { get; set; }

        public JsonExporter ExportJson { get; set; }

        private ICoffeeCompanyData Data { get; set; }

        public List<OrderInfo> GetOrderInfo()
        {
            var pendingOrders =
                from o in this.Data.Orders.All()
                join c in this.Data.ClientCompanies.All()
                on o.ClientCompanyId equals c.ID
                select new OrderInfo
                {
                    CompanyId = o.ClientCompanyId,
                    CompanyName = c.Name,
                    ProductName = o.Product.Name,
                    ProductPrice = o.Product.PricePerKgInDollars,
                    Quantity = o.QuantityInKg,
                    RevenueFromOrder = o.QuantityInKg * o.Product.PricePerKgInDollars
                };

            return pendingOrders.ToList();
        }
        private List<List<string>> GetPendingOrdersReport()
        {
            var pendingOrders =
                (from o in this.Data.Orders.Where(x => x.Status == 0)
                join c in this.Data.ClientCompanies.All()
                on o.ClientCompanyId equals c.ID
                select new
                {
                    o.ID,
                    ProductName = o.Product.Name,
                    CompanyName = c.Name,
                    TotalRevenue = o.QuantityInKg * o.Product.PricePerKgInDollars
                }).ToList();


            var formattedProducts = new List<List<string>>();

            for (int i = 0; i < pendingOrders.Count; i++)
            {
                formattedProducts.Add(new List<string>() {
                    pendingOrders[i].ID.ToString(),
                    pendingOrders[i].ProductName,
                    pendingOrders[i].CompanyName,
                    pendingOrders[i].TotalRevenue.ToString(),
                });
            }

            return formattedProducts;
        }

        public DiscountInfo GetDiscountInfo(int companyId)
        {
            var orders =
                from o in this.Data.Orders.All()
                join c in this.Data.ClientCompanies.All().Where(x => x.ID == companyId)
                on o.ClientCompanyId equals c.ID
                join p in this.Data.Products.All()
                on o.ProductId equals p.ID
                select new
                {
                    Name = c.Name,
                    Price = p.PricePerKgInDollars,
                    Quantity = o.QuantityInKg
                };

            decimal totalSpending = 0;
            DiscountType type = DiscountType.Regular;

            foreach (var order in orders)
            {
                var orderSpending = order.Price * order.Quantity;

                totalSpending += orderSpending;
            }
            
            if (totalSpending >= 10000 && totalSpending < 15000)
            {
                type = DiscountType.Silver;
            }
            else if (totalSpending >= 15000 && totalSpending < 20000)
            {
                type = DiscountType.Gold;
            }
            else if (totalSpending >= 20000)
            {
                type = DiscountType.Platium;
            }

            var discountInfo = new DiscountInfo
            {
                CompanyId = companyId,
                Type = type
            };

            return discountInfo;
        }

        private List<List<string>> GetOrdersForCompany(string companyName)
        {
            var orders =
                (from o in this.Data.Orders.Where(x => x.ClientCompany.Name == companyName)
                 select new
                 {
                     o.ID,
                     o.QuantityInKg,
                     o.Status,
                     ProductName = o.Product.Name,
                 }).ToList();

            var formattedOrderd = new List<List<string>>();
            for (int i = 0; i < orders.Count; i++)
            {
                formattedOrderd.Add(new List<string>() {
                    orders[i].ID.ToString(),
                    orders[i].QuantityInKg.ToString(),
                    orders[i].Status.ToString(),
                    orders[i].ProductName
                });
            }

            return formattedOrderd;
        }

        public void GetPendingOrdersPdfReport(string path)
        {
            var products = GetPendingOrdersReport();
            var title = "Pending Orders Report";
            var cellsTitles = new List<string> { "Order ID", "Product Name", "Company name", "Total Revenue" };

            this.ExportPdf.GetPDF(products, title, cellsTitles, path);
        }

        public void GetPendingOrdersXmlReport(string path)
        {
            var products = GetPendingOrdersReport();
            var title = "Pending Orders Report";
            var cellsTitles = new List<string> { "Order ID", "Product Name", "Company name", "Total Revenue" };

            this.ExportXml.ExportDocument(products, title, cellsTitles, path);
        }

        public void GetOrderForCompanyPdfReport(string name, string path)
        {
            var products = GetOrdersForCompany(name);
            var title = string.Format("Orders' shipment details for company \"{0}\"", name);
            var cellsTitles = new List<string> { "Order ID", "Quantity in kg", "Status", "Product Name"};

            this.ExportPdf.GetPDF(products, title, cellsTitles, path);
        }

        public void GetOrderForCompanyXmlReportd(string name, string path)
        {
            var products = GetOrdersForCompany(name);
            var title = string.Format("Orders' shipment details for company \"{0}\"", name);
            var cellsTitles = new List<string> { "Order ID", "Quantoty in kg", "Status", "Product Name" };

            this.ExportXml.ExportDocument(products, title, cellsTitles, path);
        }

        public void GetJsonOrderInfoReport() 
        {
            var orders = GetOrderInfo();
            ExportJson.ExportAllProductReports(orders);       
        }
    }
}
