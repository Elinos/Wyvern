namespace CoffeeCompany.Import
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using MongoDB.Driver;

    using CoffeeCompany.Data;
    using CoffeeCompany.Models;
    using CoffeeCompany.Import.DataLoaders;

    public class DataImport
    {
        //private const string MongoDbConnectionString = "mongodb://wyvern:coffee@kahana.mongohq.com:10019/CoffeeWyvern";
        private const string MongoDbConnectionString = "mongodb://wyvern:coffee@ds051368.mongolab.com:51368/coffeewyvern";
        private const string MongoDbName = "coffeewyvern";

        public static void ImportFromMongoDb()
        {
            var context = new CoffeeCompanyDbContext();

            try
            {
                var mongoDbLoader = new MongoDbLoader(MongoDbConnectionString, MongoDbName);

                ICollection<Order> orders;

                if (mongoDbLoader.retrieveData().Count == 0)
                {
                    mongoDbLoader.MongoDbSeed();
                }

                orders = mongoDbLoader.retrieveData();

                foreach (var order in orders)
                {
                    context.Orders.Add(order);
                }

                context.SaveChanges();
            }
            catch (MongoConnectionException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void ImportFromExcel()
        {
            var context = new CoffeeCompanyDbContext();

        }

        public static void ImportFromXml()
        {
            var context = new CoffeeCompanyDbContext();

        }
    }
}
