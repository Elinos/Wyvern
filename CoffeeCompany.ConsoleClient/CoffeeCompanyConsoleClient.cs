namespace CoffeeCompany.ConsoleClient
{
    using System;
    using System.Linq;

    using CoffeeCompany.Data;
    using CoffeeCompany.Models;
    using CoffeeCompany.Import;
    using CoffeeCompany.MySQL.Manager;
    using CoffeeCompany.SQLite.Loader;

    public class CoffeeCompanyConsoleClient
    {
        static void Main(string[] args)
        {
            //DataImport.ImportFromMongoDb();
            var mySQLManager = new MySQLManager();
            mySQLManager.AddReport("ReportOne", 2.00m, 45, 5000m);
            //var sqliteLoader = new SQLiteLoader();
            //sqliteLoader.LoadData();
        }
    }
}
