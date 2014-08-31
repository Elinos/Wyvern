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
        private const string ConnectionString = "mongodb://wyvern:coffee@kahana.mongohq.com:10019/CoffeeWyvern";
        private const string DbName = "CoffeeWyvern";
        private const string CompanyCollectionName = "Companies";

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

        public ICollection<ClientCompany> retrieveCompanies()
        {
            var companyCollection = this.mongoDb.GetCollection<ClientCompany>(CompanyCollectionName);

            var companies = companyCollection.FindAll().ToList();

            var sanitizedCompanies = new List<ClientCompany>();

            foreach (var company in companies)
            {
                sanitizedCompanies.Add(Sanitize(company));
            }

            return sanitizedCompanies;
        }

        private ClientCompany Sanitize(ClientCompany company)
        {
            var sanitizedCompany = new ClientCompany
            {
                Name = company.Name,
                CountryOfOrigin = company.CountryOfOrigin
            };

            var products = company.Products;

            foreach (var product in products)
            {
                var sanitizedProduct = new Product
                {
                    Name = product.Name,
                    PricePerKgInDollars = product.PricePerKgInDollars,
                    TypeOfCoffee = product.TypeOfCoffee
                };

                sanitizedCompany.Products.Add(sanitizedProduct);
            }

            return sanitizedCompany;
        }

        public void MongoDbSeed()
        {
            var companyCollection = this.mongoDb.GetCollection<ClientCompany>(CompanyCollectionName);

            CompaniesSeed(companyCollection);
        }

        private void CompaniesSeed(MongoCollection companyCollection)
        {
            var company1 = new ClientCompany
            {
                Name = "Coffee King",
                CountryOfOrigin = "Canada"
            };

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

            company1.Products.Add(product1);
            company1.Products.Add(product2);

            var company2 = new ClientCompany
            {
                Name = "Coffee Monkey",
                CountryOfOrigin = "Zamunda"
            };

            var product3 = new Product
            {
                Name = "CheepCoffee",
                PricePerKgInDollars = 12.98m,
                TypeOfCoffee = CoffeeTypes.Hybrid
            };

            company2.Products.Add(product3);

            var company3 = new ClientCompany
            {
                Name = "Energizer",
                CountryOfOrigin = "USA"
            };

            var product4 = new Product
            {
                Name = "Special",
                PricePerKgInDollars = 19.30m,
                TypeOfCoffee = CoffeeTypes.Arabica
            };

            company3.Products.Add(product4);

            var company4 = new ClientCompany
            {
                Name = "Morning Starter",
                CountryOfOrigin = "Italy"
            };

            var product5 = new Product
            {
                Name = "Special",
                PricePerKgInDollars = 19.30m,
                TypeOfCoffee = CoffeeTypes.Arabica
            };

            company4.Products.Add(product5);

            var company5 = new ClientCompany
            {
                Name = "StarBuzz",
                CountryOfOrigin = "USA"
            };

            var product6 = new Product
            {
                Name = "Special II",
                PricePerKgInDollars = 23.30m,
                TypeOfCoffee = CoffeeTypes.Arabica
            };

            company5.Products.Add(product6);

            companyCollection.Insert(company1);
            companyCollection.Insert(company2);
            companyCollection.Insert(company3);
            companyCollection.Insert(company4);
            companyCollection.Insert(company5);
        }
    }
}
