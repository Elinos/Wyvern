namespace CoffeeCompany.ConsoleClient
{
    using System;
    using System.Linq;

    using CoffeeCompany.Data;
    using CoffeeCompany.Models;
    using CoffeeCompany.Import;
    using CoffeeCompany.MySQL.Manager;
    using CoffeeCompany.SQLite.Loader;
    using CoffeeCompany.Excel.Manager;
    using CoffeeCompany.ReportGenerator;

    public class CoffeeCompanyConsoleClient
    {
        static void Main(string[] args)
        {
            var dataImport = new DataImport();
            //dataImport.ImportFromXml();
            //dataImport.ImportFromExcel();
            //dataImport.ImportFromMongoDb();

            var sqliteLoader = new SQLiteLoader();
            sqliteLoader.LoadData();
            var excelManager = new ExcelManager();
            excelManager.CreateExcelReport();

            //var mySQLManager = new MySQLManager();
            //mySQLManager.AddReport("ReportOne", 2.00m, 45, 5000m);
            //var sqliteLoader = new SQLiteLoader();
            //sqliteLoader.LoadData();
            //var excelManager = new ExcelManager();
            //excelManager.CreateExcelReport();

            //var data = new CoffeeCompanyData();
            //var reporter = new ReportsEngine(data);

            //reporter.GetTotalRevenuesPdfReports(@"..\..\..\Reports\TotalRevenuePdfReport.pdf");
            //reporter.GetOrderForCompany("Coffee King", @"..\..\..\Reports\CompanyOrdersPdfReport.pdf");
        }
    }
}
