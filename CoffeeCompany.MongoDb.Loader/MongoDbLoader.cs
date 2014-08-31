namespace CoffeeCompany.MongoDb.Loader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Configuration;

    using MongoDB.Driver;
    using MongoDB.Driver.Linq;
    using MongoDB.Driver.Builders;

    using Models;

    public class MongoDbLoader : IMongoDbLoader
    {
        private const string ConnectionString = "mongodb://localhost/";
        private const string DbName = "WyvernCoffee";
        private const string CollectionName = "Companies";

        private MongoDatabase GetDatabase()
        {
            var mongoClient = new MongoClient(ConnectionString);
            var mongoServer = mongoClient.GetServer();
            var database = mongoServer.GetDatabase(DbName);

            return database;
        }

        public ICollection<ClientCompany> ImportCompanies()
        {
            var database = GetDatabase();

            var companyCollection = database.GetCollection<ClientCompany>(CollectionName);

            var companies = companyCollection.FindAll().ToList();

            return companies;
        }
    }
}
