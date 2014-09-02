using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CoffeeCompany.Models;
using CoffeeCompany.MySQL.Manager;
using CoffeeCompany.MySQL.Models;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace CoffeeCompany.Excel.Manager
{
    public class ExcelManager
    {
        public void CreateExcelReport()
        {
            MySQLManager mySQLManager = new MySQLManager();
            var reports = mySQLManager.GetAllReports();
            var file = CrateDirAndFile();
            LoadReportDataToFile(file, reports);
        }

        private void LoadReportDataToFile(FileInfo file, List<Report> reports)
        {
            int currentRow = 2;
            using (ExcelPackage pck = new ExcelPackage(file))
            {
                //Add worksheet and titles
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
                ws.Cells[1, 1].Value = "Report name";
                ws.Cells[1, 2].Value = "Price";
                ws.Cells[1, 3].Value = "Number of orders";
                ws.Cells[1, 4].Value = "Total Revenue";

                //Style the titles
                using (var titles = ws.Cells[1, 1, 1, 4])
                {
                    titles.Style.Font.Bold = true;
                    titles.Style.Font.Size = 14;
                    titles.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    titles.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                }

                //Load Data
                foreach (var report in reports)
                {
                    ws.Cells[currentRow, 1].Value = report.ProductName;
                    ws.Cells[currentRow, 2].Value = report.Price;
                    ws.Cells[currentRow, 2].Style.Numberformat.Format = "0.00";
                    ws.Cells[currentRow, 3].Value = report.NumberOfOrders;
                    ws.Cells[currentRow, 4].Value = report.TotalRevenue;
                    ws.Cells[currentRow, 4].Style.Numberformat.Format = "0.00";
                    currentRow++;
                }

                //Style Data Cells
                using (var data = ws.Cells[2,1, currentRow, 4])
                {
                    data.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                }

                ws.Column(1).AutoFit();
                ws.Column(2).AutoFit();
                ws.Column(3).AutoFit();
                ws.Column(4).AutoFit();

                pck.Save();
            }
        }

        private FileInfo CrateDirAndFile()
        {
            var fileName = "Report" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
            var outputDir = @"../../../../ExcelReports/";
            Directory.CreateDirectory(outputDir);
            var file = new FileInfo(outputDir + fileName);
            return file;
        }

        public ICollection<ClientCompany> ReadClientCompanyExcelFile(string filePath)
        {
            var listOfData = new List<ClientCompany>();

            var existingFile = new FileInfo(filePath);

            using (var package = new ExcelPackage(existingFile))
            {
                ExcelWorkbook workBook = package.Workbook;

                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        ExcelWorksheet currentWorksheet = workBook.Worksheets.First();

                        for (var i = 2; i < currentWorksheet.Dimension.End.Row; i++)
                        {
                            listOfData.Add(new ClientCompany
                            {
                                Name = currentWorksheet.Cells[i, 1].Value.ToString(),
                                CountryOfOrigin = currentWorksheet.Cells[i, 2].Value.ToString()
                            });
                        }
                    }
                }
            }

            return listOfData;
        }

        public ICollection<Product> ReadProductExcelFile(string filePath)
        {
            var listOfData = new List<Product>();

            var existingFile = new FileInfo(filePath);

            using (var package = new ExcelPackage(existingFile))
            {
                ExcelWorkbook workBook = package.Workbook;

                if (workBook != null)
                {
                    if (workBook.Worksheets.Count > 0)
                    {
                        ExcelWorksheet currentWorksheet = workBook.Worksheets.First();

                        for (var i = 2; i < currentWorksheet.Dimension.End.Row; i++)
                        {
                            listOfData.Add(new Product
                            {
                                Name = currentWorksheet.Cells[i, 1].Value.ToString(),
                                PricePerKgInDollars = decimal.Parse(currentWorksheet.Cells[i, 2].Value.ToString()),
                                TypeOfCoffee = (CoffeeTypes)int.Parse(currentWorksheet.Cells[i, 3].Value.ToString())
                            });
                        }
                    }
                }
            }

            return listOfData;
        }
    }
}
