namespace CoffeeCompany.Import
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using CoffeeCompany.Data;
    using CoffeeCompany.Models;
    using CoffeeCompany.Import.DataLoaders;

    public class DataImport : IDataImport
    {
        //private const string MongoDbConnectionString = "mongodb://wyvern:coffee@kahana.mongohq.com:10019/CoffeeWyvern";
        private const string MongoDbConnectionString = "mongodb://wyvern:coffee@ds051368.mongolab.com:51368/coffeewyvern";
        private const string MongoDbName = "coffeewyvern";

        public void ImportFromMongoDb()
        {
            var context = new CoffeeCompanyDbContext();
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

        public void ImportFromExcel()
        {
            throw new NotImplementedException();
        }

        public void ImportFromXml()
        {
            throw new NotImplementedException();
        }
    }
}
