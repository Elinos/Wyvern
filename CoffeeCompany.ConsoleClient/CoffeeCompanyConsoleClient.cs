namespace CoffeeCompany.ConsoleClient
{
    using System;
    using System.Linq;

    using CoffeeCompany.Data;
    using CoffeeCompany.Models;
    using CoffeeCompany.MongoDb.Loader;

    class CoffeeCompanyConsoleClient
    {
        static void Main(string[] args)
        {
            var context = new CoffeeCompanyDbContext();

            var company = new ClientCompany()
            {
                Name = "AVG"
            };

            context.Companies.Add(company);
            context.SaveChanges();

            var savedCompany= context.Companies.First();
            Console.WriteLine("ID {0}: {1}", savedCompany.ID, savedCompany.Name);

            var mongoDbLoader = new MongoDbLoader();
            mongoDbLoader.MongoDbSeed();
        }
    }
}
