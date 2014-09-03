namespace CoffeeCompany.ReportGenerator
{
    using System.Collections.Generic;
    using System.IO;
    using CoffeeCompany.Models;
    using Newtonsoft.Json;

    public class JsonExporter
    {
        public const string PATH = @"..\..\..\Reports\Json_Reports\";
        public void ExportProductReport(OrderInfo orderInfo)
        {
            
            string orderInfoJson = JsonConvert.SerializeObject(orderInfo);
            string path = PATH + orderInfo.ProductName + ".json";
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.Write(orderInfoJson);
            }

        }

        public void ExportAllProductReports(List<OrderInfo> orders)
        {
            foreach (var order in orders)
            {
                ExportProductReport(order);
            }
        }

    }
}
