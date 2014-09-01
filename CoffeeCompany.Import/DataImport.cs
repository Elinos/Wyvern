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
        //private const string DefaultMongoDbConnectionString = "mongodb://wyvern:coffee@kahana.mongohq.com:10019/CoffeeWyvern";
        //private const string DefaultMongoDbConnectionString = "mongodb://wyvern:coffee@ds051368.mongolab.com:51368/coffeewyvern";
        private const string DefaultMongoDbConnectionString = "mongodb://localhost/";
        private const string DefaultMongoDbName = "CoffeeWyvern";

        public static void ImportFromMongoDb(
            string connectionString = DefaultMongoDbConnectionString,
            string dbName = DefaultMongoDbName)
        {
            var context = new CoffeeCompanyDbContext();

            try
            {
                var mongoDbLoader = new MongoDbLoader(connectionString, dbName);

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

            var excelLoader = new ExcelLoader();
        }

        public static void ImportFromXml()
        {
            var context = new CoffeeCompanyDbContext();

        }
    }
}
