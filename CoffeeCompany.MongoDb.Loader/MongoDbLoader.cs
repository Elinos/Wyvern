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

    public class MongoDbLoader : IDbLoader
    {
        private const string ConnectionString = "mongodb://localhost/";
        private const string DbName = "CoffeeWyvern";
        private const string CompanyCollectionName = "Companies";
        private const string ProductCollectionName = "Products";

        private MongoDatabase mongoDb;

        public MongoDbLoader()
        {
            this.mongoDb = GetDatabase();
        }

        private MongoDatabase GetDatabase()
        {
            var mongoClient = new MongoClient(ConnectionString);
            var mongoServer = mongoClient.GetServer();
            var database = mongoServer.GetDatabase(DbName);

            return database;
        }

        public ICollection<ClientCompany> ImportCompanies()
        {
            var companyCollection = this.mongoDb.GetCollection<ClientCompany>(CompanyCollectionName);

            var companies = companyCollection.FindAll().ToList();

            return companies;
        }

        public void MongoDbSeed()
        {
            var companyCollection = this.mongoDb.GetCollection<ClientCompany>(CompanyCollectionName);
            var productCollection = this.mongoDb.GetCollection<ClientCompany>(ProductCollectionName);

            CompaniesSeed(companyCollection);
            ProductSeed(productCollection);
        }

        private void CompaniesSeed(MongoCollection companyCollection)
        {
            var company1 = new ClientCompany
            {
                Name = "Coffee King",
                CountryOfOrigin = "Canada"
            };

            var company2 = new ClientCompany
            {
                Name = "Coffee Monkey",
                CountryOfOrigin = "Zamunda"
            };

            var company3 = new ClientCompany
            {
                Name = "Energizer",
                CountryOfOrigin = "USA"
            };

            var company4 = new ClientCompany
            {
                Name = "Morning Starter",
                CountryOfOrigin = "Italy"
            };

            var company5 = new ClientCompany
            {
                Name = "StarBucks",
                CountryOfOrigin = "USA"
            };

            companyCollection.Insert(company1);
            companyCollection.Insert(company2);
            companyCollection.Insert(company3);
            companyCollection.Insert(company4);
            companyCollection.Insert(company5);
        }

        private void ProductSeed(MongoCollection productCollection)
        {
            var product1 = new Product
            {
                Name = "Blue",
                PricePerKgInDollars = 17.50m,
                TypeOfCoffee = CoffeeTypes.Robusta
            };

            var product2 = new Product
            {
                Name = "Gold",
                PricePerKgInDollars = 20.12m,
                TypeOfCoffee = CoffeeTypes.Arabica
            };

            var product3 = new Product
            {
                Name = "CheepCoffee",
                PricePerKgInDollars = 12.98m,
                TypeOfCoffee = CoffeeTypes.Hybrid
            };

            var product4 = new Product
            {
                Name = "Special",
                PricePerKgInDollars = 19.30m,
                TypeOfCoffee = CoffeeTypes.Arabica
            };

            productCollection.Insert(product1);
            productCollection.Insert(product2);
            productCollection.Insert(product3);
            productCollection.Insert(product4);
        }
    }
}
