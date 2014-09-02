using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using CoffeeCompany.MySQL.Manager;
using OfficeOpenXml;

namespace CoffeeCompany.Excel.Manager
{
    public class ExcelManager
    {
        public void CreateExcelReport()
        {
            MySQLManager mySQLManager = new MySQLManager();
            var reports = mySQLManager.GetAllReports();
            var fileName = "Report.xlsx";
            var outputDir = @"../../../ExcelReports/";
            var file = new FileInfo(outputDir + fileName);
            int currentRow = 2;
            using (ExcelPackage pck = new ExcelPackage(file))
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
                ws.Cells[1, 1].Value = "Report name";
                ws.Cells[1, 2].Value = "Price";
                ws.Cells[1, 3].Value = "Number of orders";
                ws.Cells[1, 4].Value = "Total Revenue";
                foreach (var report in reports)
                {
                    ws.Cells[currentRow, 1].Value = report.ProductName;
                    ws.Cells[currentRow, 2].Value = report.Price;
                    ws.Cells[currentRow, 3].Value = report.NumberOfOrders;
                    ws.Cells[currentRow, 4].Value = report.TotalRevenue;
                    currentRow++;
                }
                pck.Save();
            }
        }
    }
}
