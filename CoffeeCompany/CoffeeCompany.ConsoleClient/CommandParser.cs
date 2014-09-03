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
    using CoffeeCompany.ConsoleClient.Renderer;


    public class CommandParser
    {
        private DataImport DataImport { get; set; }

        public MySQLManager MySqlManager { get; set; }

        public ReportsEngine ReportGenerator { get; set; }

        private ConsoleRenderer renderer = new ConsoleRenderer();

        public void ReadInitialCommand() 
        {
            renderer.PrintLegend();
            string command = Console.ReadLine();
            switch (command)
            {
                case "Export": RenderExportCommand(); break;
                case "Load": RenderLoadCommand(); break;
                default: break;
            
            }
        }

        private void RenderExportCommand() 
        {
            renderer.PrintExportLegend();
            string command = Console.ReadLine();
            switch (command)
            {
                case "Json": break;
                case "Xml": break;
                case "Pdf": break;
                case "Excel": break;
                case "Back": ReadInitialCommand(); break;
                default: break;

            }

        }

        private void RenderLoadCommand()
        {
            renderer.PrintLoadLegend();
            DataImport=new Import.DataImport();
            string command = Console.ReadLine();
            switch (command)
            {
                case "Xml": DataImport.ImportFromXml(); break;
                case "Excel": DataImport.ImportFromExcel(); break;
                case "Mongo": DataImport.ImportFromMongoDb(); break;
                case "Back": ReadInitialCommand(); break;
                default: break;

            }

        }

        //var dataImport = new DataImport();
           // dataImport.ImportFromXml();
           // dataImport.ImportFromExcel();
           // dataImport.ImportFromMongoDb();

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
