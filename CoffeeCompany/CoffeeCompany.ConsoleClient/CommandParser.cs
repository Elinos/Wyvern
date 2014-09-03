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

        private MySQLManager MySqlManager { get; set; }

        private ReportsEngine ReportGenerator { get; set; }

        private ExcelManager ExcelManager { get; set; }

        private ConsoleRenderer renderer = new ConsoleRenderer();

        private CoffeeCompanyData Data = new CoffeeCompanyData();

        private SQLiteLoader sqliteLoader = new SQLiteLoader();

        public void InitiateCommandMenu() 
        {
            renderer.PrintLegend();
            string command = Console.ReadLine();
            command.ToLower();
            switch (command)
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
            command.ToLower();
            switch (command)
            {
                case "json": this.ReportGenerator.GetJSONProductReport(); break;
                case "xml": ParseXmlExport(); break;
                case "pdf": ParsePdfExport(); break;
                case "excel": this.ExcelManager.CreateExcelReport(); break;
                case "back": InitiateCommandMenu(); break;
                default: renderer.InvalidCommandMessage(); break;

            }

        }

        private void ParseLoadCommand()
        {
            renderer.PrintLoadLegend();
            this.DataImport = new DataImport();
            string command = Console.ReadLine();
            command.ToLower();
            switch (command)
            {
                case "xml": this.DataImport.ImportFromXml(); break;
                case "excel": this.DataImport.ImportFromExcel(); break;
                case "mongo": this.DataImport.ImportFromMongoDb(); break;
                case "back": InitiateCommandMenu(); break;
                default: renderer.InvalidCommandMessage(); break;

            }

        }

        private void ParseXmlExport()
        {
            renderer.PrintCustomReportLegend();
            string command = Console.ReadLine();
            command.ToLower();
            switch (command)
            {
                case "revenue": this.ReportGenerator.GetTotalRevenuesXmlReports(@"..\..\..\Reports\TotalRevenuePdfReport.xml"); break;
                case "order": this.ReportGenerator.GetOrderForCompanyXmlReport("Coffee King", @"..\..\..\Reports\CompanyOrdersPdfReport.xml"); break;
                case "back": ParseExportCommand(); break;
                default: renderer.InvalidCommandMessage(); break;

            }
        
        }

        private void ParsePdfExport()
        {
            renderer.PrintCustomReportLegend();
            string command = Console.ReadLine();
            command.ToLower();
            switch (command)
            {
                case "revenue": this.ReportGenerator.GetTotalRevenuesPdfReports(@"..\..\..\Reports\TotalRevenuePdfReport.xml"); break;
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
