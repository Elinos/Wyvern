namespace CoffeeCompany.Import.DataLoaders
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MongoDB.Driver;

    using Models;
    using CoffeeCompany.Import.DataLoaders.Contracts;

    internal class MongoDbLoader : IDataLoader
    {
        private const string OrderCollectionName = "Orders";
        private const string ClientCompanyCollectionName = "Companies";
        private const string ProductCollectionName = "Products";

        private string dbName;
        private MongoDatabase mongoDb;

        public MongoDbLoader(string connectionString, string dbName)
        {
            this.dbName = dbName;
            this.mongoDb = GetDatabase(connectionString);
        }

        private MongoDatabase GetDatabase(string connectionString)
        {
            var mongoClient = new MongoClient(connectionString);
            var mongoServer = mongoClient.GetServer();
            var database = mongoServer.GetDatabase(dbName);

            return database;
        }

        public ICollection<Order> retrieveOrdersData()
        {
            var orderCollection = this.mongoDb.GetCollection<Order>(OrderCollectionName);

            var orders = orderCollection.FindAll().ToList();

            return orders;
        }

        public ICollection<ClientCompany> retrieveCompaniesData()
        {
            var clientCompanyCollection = this.mongoDb.GetCollection<ClientCompany>(ClientCompanyCollectionName);

            var clientCompanies = clientCompanyCollection.FindAll().ToList();

            return clientCompanies;
        }

        public ICollection<Product> retrieveProductsData()
        {
            var productCollection = this.mongoDb.GetCollection<Product>(ProductCollectionName);

            var produtcs = productCollection.FindAll().ToList();

            return produtcs;
        }

        public void MongoDbSeed()
        {
            var orderCollection = this.mongoDb.GetCollection<Order>(OrderCollectionName);
            var clientCompanyCollection = this.mongoDb.GetCollection<ClientCompany>(ClientCompanyCollectionName);
            var productCollection = this.mongoDb.GetCollection<Product>(ProductCollectionName);

            var company1 = new
            {
                Name = "Coffee King",
                CountryOfOrigin = "Canada"
            };

            var company2 = new
            {
                Name = "Coffee Monkey",
                CountryOfOrigin = "Zamunda"
            };

            var company3 = new
            {
                Name = "Energizer",
                CountryOfOrigin = "USA"
            };

            var company4 = new
            {
                Name = "Morning Starter",
                CountryOfOrigin = "Italy"
            };

            var company5 = new
            {
                Name = "StarBuzz",
                CountryOfOrigin = "USA"
            };

            var product1 = new
            {
                Name = "Blue",
                PricePerKgInDollars = 17.50m,
                TypeOfCoffee = CoffeeTypes.Robusta
            };

            var product2 = new
            {
                Name = "Gold",
                PricePerKgInDollars = 20.12m,
                TypeOfCoffee = CoffeeTypes.Arabica
            };

            var product3 = new
            {
                Name = "CheepCoffee",
                PricePerKgInDollars = 12.98m,
                TypeOfCoffee = CoffeeTypes.Hybrid
            };

            var product4 = new
            {
                Name = "Special",
                PricePerKgInDollars = 19.30m,
                TypeOfCoffee = CoffeeTypes.Arabica
            };

            var product5 = new
            {
                Name = "Special III",
                PricePerKgInDollars = 19.30m,
                TypeOfCoffee = CoffeeTypes.Arabica
            };

            var product6 = new
            {
                Name = "Special II",
                PricePerKgInDollars = 23.30m,
                TypeOfCoffee = CoffeeTypes.Arabica
            };

            var order1 = new
            {
                ClientCompany = company1,
                Products = product1,
                QuantityInKg = 100,
                Status = OrderStatus.Pending
            };

            var order2 = new
            {
                ClientCompany = company2,
                Products = product2,
                QuantityInKg = 500,
                Status = OrderStatus.Processed
            };

            var order3 = new
            {
                ClientCompany = company3,
                Products = product3,
                QuantityInKg = 240,
                Status = OrderStatus.Shipped
            };

            var order4 = new
            {
                ClientCompany = company4,
                Products = product4,
                QuantityInKg = 410,
                Status = OrderStatus.Returned
            };

            var order5 = new
            {
                ClientCompany = company5,
                Products = product4,
                QuantityInKg = 220,
                Status = OrderStatus.Returned
            };

            orderCollection.Insert(order1);
            orderCollection.Insert(order2);
            orderCollection.Insert(order3);
            orderCollection.Insert(order4);
            orderCollection.Insert(order5);

            clientCompanyCollection.Insert(company1);
            clientCompanyCollection.Insert(company2);
            clientCompanyCollection.Insert(company3);
            clientCompanyCollection.Insert(company4);
            clientCompanyCollection.Insert(company5);

            productCollection.Insert(product1);
            productCollection.Insert(product2);
            productCollection.Insert(product3);
            productCollection.Insert(product4);
            productCollection.Insert(product5);
            productCollection.Insert(product6);
        }
    }
}
