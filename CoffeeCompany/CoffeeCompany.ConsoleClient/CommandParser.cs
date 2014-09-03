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

        private CoffeeCompanyData Data = new CoffeeCompanyData();

        public void InitiateCommandMenu() 
        {
            renderer.PrintLegend();
            string command = Console.ReadLine();
            switch (command)
            {
                case "Export": ParseExportCommand(); break;
                case "Load": ParseLoadCommand(); break;
                default: renderer.InvalidCommandMessage(); break;
            
            }
        }

        private void ParseExportCommand() 
        {
            renderer.PrintExportLegend();
            this.ReportGenerator = new ReportsEngine(Data);
            string command = Console.ReadLine();
            switch (command)
            {
                case "Json": this.ReportGenerator.GetJSONProductReport(); break;
                case "Xml": ParseXmlExport(); break;
                case "Pdf": ParsePdfExport(); break;
                case "Excel": break;
                case "Back": InitiateCommandMenu(); break;
                default: renderer.InvalidCommandMessage(); break;

            }

        }

        private void ParseLoadCommand()
        {
            renderer.PrintLoadLegend();
            this.DataImport = new DataImport();
            string command = Console.ReadLine();
            switch (command)
            {
                case "Xml": this.DataImport.ImportFromXml(); break;
                case "Excel": this.DataImport.ImportFromExcel(); break;
                case "Mongo": this.DataImport.ImportFromMongoDb(); break;
                case "Back": InitiateCommandMenu(); break;
                default: renderer.InvalidCommandMessage(); break;

            }

        }

        private void ParseXmlExport()
        {
            renderer.PrintCustomReportLegend();
            string command = Console.ReadLine();
            switch (command)
            {
                case "Revenue": this.ReportGenerator.GetTotalRevenuesXmlReports(@"..\..\..\Reports\TotalRevenuePdfReport.xml"); break;
                case "Order": this.ReportGenerator.GetOrderForCompanyXmlReport("Coffee King", @"..\..\..\Reports\CompanyOrdersPdfReport.xml"); break;
                case "Back": ParseExportCommand(); break;
                default: renderer.InvalidCommandMessage(); break;

            }
        
        }

        private void ParsePdfExport()
        {
            renderer.PrintCustomReportLegend();
            string command = Console.ReadLine();
            switch (command)
            {
                case "Revenue": this.ReportGenerator.GetTotalRevenuesPdfReports(@"..\..\..\Reports\TotalRevenuePdfReport.xml"); break;
                case "Order": this.ReportGenerator.GetOrderForCompanyPdfReport(("Coffee King", @"..\..\..\Reports\CompanyOrdersPdfReport.xml"); break;
                case "Back": ParseExportCommand(); break;
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
