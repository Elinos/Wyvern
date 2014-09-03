namespace CoffeeCompany.ReportGenerator
{
    using System.Collections.Generic;
    using System.IO;
    using CoffeeCompany.Models;
    using Newtonsoft.Json;

    public class JsonExporter
    {
        public const string PATH = "Json-Reports/";

        public void ExportProductReport(Product product)
        {
            string productJson = JsonConvert.SerializeObject(product);
            string path = PATH + product.ID + ".json";
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(productJson);
            }

        }

        public void ExportAllProductReports(List<Product> products)
        {
            foreach (var product in products)
            {
                ExportProductReport(product);
            }
        }

    }
}
