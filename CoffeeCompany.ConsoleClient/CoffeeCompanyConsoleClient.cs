namespace CoffeeCompany.ConsoleClient
{
    using System;
    using System.Linq;

    using CoffeeCompany.Data;
    using CoffeeCompany.Models;
    using CoffeeCompany.Import;

    public class CoffeeCompanyConsoleClient
    {
        static void Main(string[] args)
        {
            var dataImport = new DataImport();

            dataImport.ImportFromMongoDb();

        }
    }
}
