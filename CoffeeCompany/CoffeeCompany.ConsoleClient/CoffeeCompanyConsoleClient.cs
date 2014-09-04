namespace CoffeeCompany.ConsoleClient
{
    using System;
    using System.Linq;

    using CoffeeCompany.Data;
    using CoffeeCompany.Models;
    using CoffeeCompany.Import;
    using CoffeeCompany.MySQL.Manager;
    using CoffeeCompany.SQLite.Manager;
    using CoffeeCompany.Excel.Manager;
    using CoffeeCompany.ReportGenerator;

    public class CoffeeCompanyConsoleClient
    {
        static void Main(string[] args)
        {
            CommandParser parser = new CommandParser();

            parser.InitiateCommandMenu();
            //var dataImport = new DataImport();
            //dataImport.ImportFromXml();
           // dataImport.ImportFromExcel();
            //dataImport.ImportFromMongoDb();

            //var mySQLManager = new MySQLManager();
            //mySQLManager.AddReport(1 , "company1", "product1", "gosho", 2.00m, 45, 5000m);
            // var sqliteManager = new SQLiteManager();
            //sqliteManager.LoadData();
            //var excelManager = new ExcelManager();
           // excelManager.CreateExcelReport();

            //var data = new CoffeeCompanyData();
            //var reporter = new ReportsEngine(data);
            //var reporter = new ReportsEngine();

            //reporter.GetDiscountInfo(6);

            //reporter.GetTotalRevenuesPdfReports(@"..\..\..\Reports\TotalRevenuePdfReport.pdf");
            //reporter.GetOrderForCompany("Coffee King", @"..\..\..\Reports\CompanyOrdersPdfReport.pdf");
        }
    }
}
