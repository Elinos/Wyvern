namespace CoffeeCompany.ConsoleClient
{
    using System;
    using System.Linq;

    using CoffeeCompany.Data;
    using CoffeeCompany.Models;
    using CoffeeCompany.MongoDb.Loader;

    public class DataImport
    {
        public static void ImportFromMongoDb()
        {
            var context = new CoffeeCompanyDbContext();
            var mongoDbLoader = new MongoDbLoader();

            if (mongoDbLoader.retrieveCompanies().Count == 0 || mongoDbLoader.retrieveProducts().Count == 0)
            {
                mongoDbLoader.MongoDbSeed();
            }

            var companies = mongoDbLoader.retrieveCompanies();
            var products = mongoDbLoader.retrieveProducts();

            foreach (var company in companies)
            {
                context.Companies.Add(company);
            }

            foreach (var product in products)
            {
                context.Products.Add(product);
            }

            context.SaveChanges();
        }
    }
}
