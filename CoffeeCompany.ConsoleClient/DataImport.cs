namespace CoffeeCompany.ConsoleClient
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    using CoffeeCompany.Data;
    using CoffeeCompany.Models;
    using CoffeeCompany.MongoDb.Loader;

    public class DataImport
    {
        public static void ImportFromMongoDb()
        {
            var context = new CoffeeCompanyDbContext();
            var mongoDbLoader = new MongoDbLoader();

            ICollection<ClientCompany> companies;

            if (mongoDbLoader.retrieveData().Count == 0)
            {
                mongoDbLoader.MongoDbSeed();
            }

            companies = mongoDbLoader.retrieveData();

            foreach (var company in companies)
            {
                context.Companies.Add(company);
            }

            context.SaveChanges();
        }
    }
}
