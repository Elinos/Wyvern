namespace CoffeeCompany.Import.DataLoaders
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

    public class MongoDbLoader : IDbLoader<Order>
    {
        private const string OrderCollectionName = "Orders";

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

        public ICollection<Order> retrieveData()
        {
            var orderCollection = this.mongoDb.GetCollection<Order>(OrderCollectionName);

            var orders = orderCollection.FindAll().ToList();

            var sanitizedOrders = new HashSet<Order>();

            foreach (var order in orders)
            {
                sanitizedOrders.Add(Sanitize(order));
            }

            return sanitizedOrders;
        }

        private Order Sanitize(Order order)
        {
            var sanitizedOrder = new Order
            {
                ClientCompany = order.ClientCompany,
                QuantityInKg = order.QuantityInKg,
                Status = order.Status
            };

            var products = order.Products;

            foreach (var product in products)
            {
                var sanitizedProduct = new Product
                {
                    Name = product.Name,
                    PricePerKgInDollars = product.PricePerKgInDollars,
                    TypeOfCoffee = product.TypeOfCoffee
                };

                sanitizedOrder.Products.Add(sanitizedProduct);
            }

            return sanitizedOrder;
        }

        public void MongoDbSeed()
        {
            var orderCollection = this.mongoDb.GetCollection<Order>(OrderCollectionName);

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
                Name = "Special",
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
                Products = new HashSet<object>(),
                QuantityInKg = 100,
                Status = OrderStatus.Pending
            };

            var order2 = new
            {
                ClientCompany = company2,
                Products = new HashSet<object>(),
                QuantityInKg = 500,
                Status = OrderStatus.Processed
            };

            var order3 = new
            {
                ClientCompany = company3,
                Products = new HashSet<object>(),
                QuantityInKg = 240,
                Status = OrderStatus.Shipped
            };

            var order4 = new
            {
                ClientCompany = company4,
                Products = new HashSet<object>(),
                QuantityInKg = 410,
                Status = OrderStatus.Returned
            };

            var order5 = new
            {
                ClientCompany = company5,
                Products = new HashSet<object>(),
                QuantityInKg = 220,
                Status = OrderStatus.Returned
            };

            order1.Products.Add(product1);
            order1.Products.Add(product2);
            order2.Products.Add(product3);
            order3.Products.Add(product4);
            order4.Products.Add(product5);
            order5.Products.Add(product6);

            orderCollection.Insert(order1);
            orderCollection.Insert(order2);
            orderCollection.Insert(order3);
            orderCollection.Insert(order4);
            orderCollection.Insert(order5);
        }
    }
}
