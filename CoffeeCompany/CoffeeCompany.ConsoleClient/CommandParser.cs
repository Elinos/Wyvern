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
    using CoffeeCompany.ConsoleClient.Renderer;


    public class CommandParser
    {
        private DataImport DataImport { get; set; }

        private MySQLManager MySqlManager { get; set; }

        private ReportsEngine ReportGenerator { get; set; }

        private ExcelManager ExcelManager { get; set; }

        private ConsoleRenderer renderer = new ConsoleRenderer();

        private CoffeeCompanyData Data = new CoffeeCompanyData();

        private SQLiteManager sqliteLoader = new SQLiteManager();

        public void InitiateCommandMenu() 
        {
            renderer.PrintLegend();
            string command = Console.ReadLine();
            switch (command.ToLower())
            {
                case "export": ParseExportCommand(); break;
                case "load": ParseLoadCommand(); break;
                default: renderer.InvalidCommandMessage(); break;
            
            }
        }

        private void ParseExportCommand() 
        {
            renderer.PrintExportLegend();
            this.ReportGenerator = new ReportsEngine(Data);
            this.ExcelManager = new ExcelManager();
            string command = Console.ReadLine();
            switch (command.ToLower())
            {
                case "json":
                    {
                        this.ReportGenerator.GetJsonOrderInfoReport();
                        this.renderer.CompletedMessage();
                        ParseExportCommand();
                        break;
                    }
                case "xml":
                    {
                        ParseXmlExport();
                        this.renderer.CompletedMessage();
                        ParseExportCommand();
                        break;
                    }
                case "excel":
                    {
                        this.ExcelManager.CreateExcelReport();
                        this.renderer.CompletedMessage();
                        ParseExportCommand();
                        break;
                    }
                case "mysql":
                    {
                        MySqlManager.LoadAllReportsDataFromSQLServer();
                        this.renderer.CompletedMessage();
                        ParseExportCommand();
                        break;
                    }
                case "back": InitiateCommandMenu(); break;
                default: renderer.InvalidCommandMessage(); break;

            }

        }

        private void ParseLoadCommand()
        {
            renderer.PrintLoadLegend();
            this.DataImport = new DataImport();
            string command = Console.ReadLine();
            switch (command.ToLower())
            {
                case "xml": { 
                    this.DataImport.ImportFromXml();
                    this.renderer.CompletedMessage();
                    ParseLoadCommand();
                    break; }
                case "excel":
                    {
                        this.DataImport.ImportFromExcel();
                        this.renderer.CompletedMessage();
                        ParseLoadCommand();
                        break;
                    }
                case "mongo":
                    {
                        this.DataImport.ImportFromMongoDb();
                        this.renderer.CompletedMessage();
                        ParseLoadCommand();
                        break;
                    }
                case "back":
                    {
                        InitiateCommandMenu();
                        this.renderer.CompletedMessage();
                        ParseLoadCommand();
                        break;
                    }
                default: renderer.InvalidCommandMessage(); break;

            }

        }

        private void ParseXmlExport()
        {
            renderer.PrintCustomReportLegend();
            string command = Console.ReadLine();
            switch (command.ToLower())
            {
                case "pending": this.ReportGenerator.GetPendingOrdersXmlReport(@"..\..\..\Reports\PendingOrdersXmlReport.xml"); break;
                case "order": this.ReportGenerator.GetOrderForCompanyXmlReport("Coffee King", @"..\..\..\Reports\CompanyOrdersPdfReport.xml"); break;
                case "back": ParseExportCommand(); break;
                default: renderer.InvalidCommandMessage(); break;

            }
        
        }

        private void ParsePdfExport()
        {
            renderer.PrintCustomReportLegend();
            string command = Console.ReadLine();
            switch (command.ToLower())
            {
                case "pending": this.ReportGenerator.GetPendingOrdersPdfReport(@"..\..\..\Reports\PendingOrdersPdfReport.xml"); break;
                case "order": this.ReportGenerator.GetOrderForCompanyPdfReport("Coffee King", @"..\..\..\Reports\CompanyOrdersPdfReport.xml"); break;
                case "back": ParseExportCommand(); break;
                default: renderer.InvalidCommandMessage(); break;

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
