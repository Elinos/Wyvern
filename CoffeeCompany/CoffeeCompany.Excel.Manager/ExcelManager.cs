namespace CoffeeCompany.Excel.Manager
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using CoffeeCompany.Models;
    using CoffeeCompany.MySQL.Manager;
    using CoffeeCompany.SQLite.Manager;
    using OfficeOpenXml;
    using OfficeOpenXml.Style;
    using System.Drawing;

    public class ExcelManager
    {
        public bool CreateExcelReport()
        {
            bool result;
            try
            {
                var mySQLManager = new MySQLManager();
                var reports = mySQLManager.GetAllReports();
                var sqliteManager = new SQLiteManager();
                var discountInformations = sqliteManager.GetDiscountPercentagesPerCompany();
                var reportsWithDiscounts = from r in reports
                                           join di in discountInformations on r.CompanyID equals di.CompanyID
                                           select new DiscountedReport
                                           {
                                               CompanyName = r.CompanyName,
                                               ProductName = r.ProductName,
                                               Price = r.Price * (decimal)(1 - (di.DiscountPercent / 100.00)),
                                               Quantity = r.Quantity,
                                               TotalRevenue = r.TotalRevenue * (decimal)(1 - (di.DiscountPercent / 100.00)),
                                               TotalDiscount = r.TotalRevenue * (decimal)(di.DiscountPercent / 100.00)
                                           };
                var file = CreateDirAndFile();
                WriteReportDataToFile(file, reportsWithDiscounts);
                result = true;
            }
            catch (Exception)
            {
                result = false;
            }
            return result;
        }

        private void WriteReportDataToFile(FileInfo file, IEnumerable<DiscountedReport> reportsWithDiscounts)
        {
            int currentRow = 2;
            using (ExcelPackage pck = new ExcelPackage(file))
            {
                //Add worksheet and titles
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");
                ws.Cells[1, 1].Value = "Company name";
                ws.Cells[1, 2].Value = "Product name";
                ws.Cells[1, 3].Value = "Price";
                ws.Cells[1, 4].Value = "Quantity";
                ws.Cells[1, 5].Value = "Total Revenue";
                ws.Cells[1, 6].Value = "Total Discount";

                //Style the titles
                using (var titles = ws.Cells[1, 1, 1, 6])
                {
                    titles.Style.Font.Bold = true;
                    titles.Style.Font.Size = 14;
                    titles.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    titles.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    titles.Style.VerticalAlignment = ExcelVerticalAlignment.Center;
                    titles.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    var border = titles.Style.Border;
                    border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                }

                //Load Data
                foreach (var report in reportsWithDiscounts)
                {
                    ws.Cells[currentRow, 1].Value = report.CompanyName;
                    ws.Cells[currentRow, 2].Value = report.ProductName;
                    ws.Cells[currentRow, 3].Value = report.Price;
                    ws.Cells[currentRow, 3].Style.Numberformat.Format = "0.00";
                    ws.Cells[currentRow, 4].Value = report.Quantity;
                    ws.Cells[currentRow, 4].Style.Numberformat.Format = "0.00";
                    ws.Cells[currentRow, 5].Value = report.TotalRevenue;
                    ws.Cells[currentRow, 5].Style.Numberformat.Format = "0.00";
                    ws.Cells[currentRow, 6].Value = report.TotalDiscount;
                    ws.Cells[currentRow, 6].Style.Numberformat.Format = "0.00";

                    currentRow++;
                }
                if (reportsWithDiscounts.Count() > 0)
                {
                    //Style Data Cells
                    using (var data = ws.Cells[2, 1, currentRow - 1, 6])
                    {
                        data.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                        var border = data.Style.Border;
                        border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                    }
                }
                ws.Column(1).AutoFit();
                ws.Column(2).AutoFit();
                ws.Column(3).AutoFit();
                ws.Column(4).AutoFit();
                ws.Column(5).AutoFit();
                ws.Column(6).AutoFit();

                pck.Save();
            }
        }

        private FileInfo CreateDirAndFile()
        {
            var fileName = "Report" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
            var outputDir = @"../../../../Reports/ExcelReports/";
            Directory.CreateDirectory(outputDir);
            var file = new FileInfo(outputDir + fileName);
            return file;
        }

        public ICollection<ClientCompany> ReadClientCompanyExcelFile(string filePath)
        {
            var listOfData = new List<ClientCompany>();

            try
            {
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
            }
            catch (Exception e)
            {
                Console.WriteLine("File {0} doesn't exist", filePath);
                Console.WriteLine(e.Message);
            }

            return listOfData;
        }

        public ICollection<Product> ReadProductExcelFile(string filePath)
        {
            var listOfData = new List<Product>();

            try
            {
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
            }
            catch (Exception e)
            {
                Console.WriteLine("File {0} doesn't exist", filePath);
                Console.WriteLine(e.Message);
            }

            return listOfData;
        }

        public IEnumerable<Order> ReadOrderExcelFile(string filePath)
        {
            var listOfData = new List<Order>();

            try
            {
                var existingFile = new FileInfo(filePath);

                using (var package = new ExcelPackage(existingFile))
                {
                    ExcelWorkbook workBook = package.Workbook;

                    if (workBook != null)
                    {
                        if (workBook.Worksheets.Count > 0)
                        {
                            ExcelWorksheet currentWorksheet = workBook.Worksheets.First();

                            for (var i = 2; i < currentWorksheet.Dimension.End.Row + 1; i++)
                            {
                                var product = new Product
                                    {
                                        Name = currentWorksheet.Cells[i, 2].Value.ToString()
                                    };

                                var currentCompany = new ClientCompany
                                    {
                                        Name = currentWorksheet.Cells[i, 1].Value.ToString()
                                    };

                                var employee = new Employee
                                    {
                                        Username = currentWorksheet.Cells[i, 7].Value.ToString()
                                    };

                                var currentOrder = new Order
                                {
                                    ClientCompany = currentCompany,
                                    Product = product,
                                    QuantityInKg = int.Parse(currentWorksheet.Cells[i, 4].Value.ToString()),
                                    Employee = employee
                                };

                                listOfData.Add(currentOrder);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("File {0} doesn't exist", filePath);
                Console.WriteLine(e.Message);
            }

            return listOfData;
        }
    }
}
