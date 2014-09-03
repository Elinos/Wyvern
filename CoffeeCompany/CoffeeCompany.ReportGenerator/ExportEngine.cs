namespace CoffeeCompany.ReportGenerator
{
    using System;

    using CoffeeCompany.Data;
    using System.Linq;
    using CoffeeCompany.Models;
    using CoffeeCompany.ReportGenerator;
    using System.Collections.Generic;
    using System.Collections;

    public class ReportsEngine
    {
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

        private List<List<string>> GetTotalRevenuesFromDatabase(string path)
        {                      
            var products =
                (from p in Data.Products.All()
                from o in p.Orders
                select new
                {
                    Name = p.Name,
                    Price = p.PricePerKgInDollars.ToString(),
                    Quantity = o.QuantityInKg.ToString(), 
                    Total = (p.PricePerKgInDollars * o.QuantityInKg).ToString()
                }).ToList();

            var formattedProducts = new List<List<string>>();

            for (int i = 0; i < products.Count; i++)
            {
                formattedProducts.Add(new List<string>() {
                    products[i].Name,
                    products[i].Price.ToString(),
                    products[i].Quantity.ToString(),
                    products[i].Total.ToString(),
                });
            }

            return formattedProducts;
        }    

        private List<List<string>> GetOrdersForCompany(string companyName)
        {
            int companyId = (from c in this.Data.ClientCompanies.All()
                            where c.Name == companyName
                            select c.ID).First();

            var orders =
                (from o in this.Data.Orders.All()
                 where o.ClientCompanyId == companyId
                 select new
                 {
                     o.ID,
                     o.QuantityInKg,
                     o.Status,
                     Products = o.Products.Select(p => p.Name),
                 }).ToList();

            var formattedOrderd = new List<List<string>>();
            for (int i = 0; i < orders.Count; i++)
            {
                formattedOrderd.Add(new List<string>() {
                    orders[i].ID.ToString(),
                    orders[i].QuantityInKg.ToString(),
                    orders[i].Status.ToString(),
                    string.Join(", ", orders[i].Products),
                });
            }

            return formattedOrderd;
        }

        public void GetTotalRevenuesPdfReports(string path)
        {
            var products = GetTotalRevenuesFromDatabase(path);
            var title = "Total Revenue Report";
            var cellsTitles = new List<string> { "Product Name", "Product Price", "Number of orders", "Total Revenue" };

            this.ExportPdf.GetPDF(products, title, cellsTitles, path);
        }

        public void GetTotalRevenuesXmlReports(string path)
        {
            var products = GetTotalRevenuesFromDatabase(path);
            var title = "Total Revenue Report";
            var cellsTitles = new List<string> { "Product Name", "Product Price", "Number of orders", "Total Revenue" };

            this.ExportXml.ExportDocument(products, title, cellsTitles, path);
        }

        public void GetOrderForCompanyPdfReport(string name, string path)
        {
            var products = GetOrdersForCompany(name);
            var title = string.Format("Orders' shipment details for company \"{0}\"", name);
            var cellsTitles = new List<string> { "Order ID", "Quantoty in kg", "Status", "Products"};

            this.ExportPdf.GetPDF(products, title, cellsTitles, path);
        }

        public void GetOrderForCompanyXmlReport(string name, string path)
        {
            var products = GetOrdersForCompany(name);
            var title = string.Format("Orders' shipment details for company \"{0}\"", name);
            var cellsTitles = new List<string> { "Order ID", "Quantoty in kg", "Status", "Products" };

            this.ExportXML.ExportDocument(products, title, cellsTitles, path);
        }
    }
}
