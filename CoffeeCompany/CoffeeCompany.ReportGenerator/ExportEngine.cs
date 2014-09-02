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
        const string defaultServer = "Server=.\\SQLEXPRESS;Database=CoffeeCompanyConnection; Trusted_Connection=true;";
        public ReportsEngine()
        {
            this.ExportPdf = new PDFExporter(defaultServer);
        }
        public PDFExporter ExportPdf { get; set; }

        public void GetTotalRevenuesPdfReports(string path)
        {
            var data = new CoffeeCompanyData();
            
            var products =
                (from p in data.Products.All()
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
                    products[i].Price,
                    products[i].Quantity,
                    products[i].Total,
                });
            }
            var title = "Total Revenue Report";
            var cellsTitles = new List<string> { "Product Name", "Product Price", "Number of orders", "Total Revenue" };

            this.ExportPdf.GetPDF(formattedProducts, title, cellsTitles, path);
        }
    }
}
