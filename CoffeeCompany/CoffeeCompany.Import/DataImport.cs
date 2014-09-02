namespace CoffeeCompany.Import
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using MongoDB.Driver;

    using CoffeeCompany.Data;
    using CoffeeCompany.Models;
    using CoffeeCompany.Import.DataLoaders;

    public class DataImport
    {
        private const string DefaultMongoDbConnectionString = "mongodb://wyvern:coffee@kahana.mongohq.com:10019/CoffeeWyvern";
        //private const string DefaultMongoDbConnectionString = "mongodb://wyvern:coffee@ds051368.mongolab.com:51368/coffeewyvern";
        //private const string DefaultMongoDbConnectionString = "mongodb://localhost/";
        private const string DefaultMongoDbName = "CoffeeWyvern";

        private const string DefaultZipToUnpack = @"..\..\..\..\dbData\ExcelData.zip";
        private const string DefaultUnpackDirectory = @"..\..\..\..\dbData\ExcelData";

        private const string DefaultXMLDataFileName = @"..\..\..\..\dbData\CoffeeCompanyData.xml";

        private ICoffeeCompanyDbContext context;

        public DataImport(ICoffeeCompanyDbContext context)
        {
            this.context = context;
        }

        public DataImport()
            : this(new CoffeeCompanyDbContext())
        {
        }

        public void ImportFromMongoDb(
            string connectionString = DefaultMongoDbConnectionString,
            string dbName = DefaultMongoDbName)
        {
            try
            {
                var mongoDbLoader = new MongoDbLoader(connectionString, dbName);

                var orders = mongoDbLoader.retrieveOrdersData();

                //Seed to mongodb database, if there're no data
                if (orders.Count == 0)
                {
                    mongoDbLoader.MongoDbSeed();
                    orders = mongoDbLoader.retrieveOrdersData();
                }

                foreach (var order in orders)
                {
                    var mergedOrder = this.MergeWithExistingOrders(order);

                    this.context.Orders.Add(mergedOrder);
                }

                this.context.SaveChanges();
            }
            catch (MongoConnectionException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ImportFromExcel(
            string zipToUnpack = DefaultZipToUnpack,
            string unpackDirectory = DefaultUnpackDirectory)
        {
            try
            {
                var excelLoader = new ExcelLoader(zipToUnpack, unpackDirectory);

                var companies = excelLoader.retrieveCompaniesData();
                var products = excelLoader.retrieveProductsData();

                foreach (var company in companies)
                {
                    if (this.context.ClientCompanies.Any(c => c.Name == company.Name))
                    {
                        continue;
                    }

                    this.context.ClientCompanies.Add(company);
                }

                foreach (var product in products)
                {
                    if (context.Products.Any(p => p.Name == product.Name))
                    {
                        continue;
                    }

                    this.context.Products.Add(product);
                }

                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void ImportFromXml(
            string xmlFilePath = DefaultXMLDataFileName)
        {
            try
            {
                var xmlLoader = new XmlLoader(xmlFilePath);

                var orders = xmlLoader.retrieveOrdersData();

                foreach (var order in orders)
                {
                    var mergedOrder = this.MergeWithExistingOrders(order);

                    this.context.Orders.Add(mergedOrder);
                }

                this.context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private Order MergeWithExistingOrders(Order order)
        {
            var mergedOrder = new Order
                {
                    ClientCompany = this.MergeWithExistingClientCompanies(order.ClientCompany),
                    QuantityInKg = order.QuantityInKg,
                    Status = order.Status,
                    Products = new HashSet<Product>()
                };

            foreach(var product in order.Products)
            {
                Product mergedProduct = this.MergeWithExistingProducts(product);
                mergedOrder.Products.Add(mergedProduct);
            }

            return mergedOrder;
        }

        private ClientCompany MergeWithExistingClientCompanies(ClientCompany clientCompany)
        {
            var mergedClientCompany = this.context.ClientCompanies.Where(c => c.Name == clientCompany.Name).FirstOrDefault();

            if (mergedClientCompany != null)
            {
                return mergedClientCompany;
            }

            return clientCompany;
        }

        private Product MergeWithExistingProducts(Product product)
        {
            var mergedProduct = this.context.Products.Where(p => p.Name == product.Name).FirstOrDefault();

            if (mergedProduct != null)
            {
                return mergedProduct;
            }

            return product;
        }
    }
}
