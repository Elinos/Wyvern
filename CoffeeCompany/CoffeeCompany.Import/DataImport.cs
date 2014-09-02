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
        //private const string DefaultMongoDbConnectionString = "mongodb://wyvern:coffee@kahana.mongohq.com:10019/CoffeeWyvern";
        //private const string DefaultMongoDbConnectionString = "mongodb://wyvern:coffee@ds051368.mongolab.com:51368/coffeewyvern";
        private const string DefaultMongoDbConnectionString = "mongodb://localhost/";
        private const string DefaultMongoDbName = "CoffeeWyvern";

        private const string DefaultZipToUnpack = @"..\..\..\..\dbData\ExcelData.zip";
        private const string DefaultUnpackDirectory = @"..\..\..\..\dbData\ExcelData";

        private const string DefaultXMLDataFileName = @"..\..\..\..\dbData\CoffeeCompanyData.xml";

        public static void ImportFromMongoDb(
            string connectionString = DefaultMongoDbConnectionString,
            string dbName = DefaultMongoDbName)
        {
            var context = new CoffeeCompanyDbContext();

            try
            {
                var mongoDbLoader = new MongoDbLoader(connectionString, dbName);

                var orders = mongoDbLoader.retrieveData();

                //Seed to mongodb database, if there're no data
                if (orders.Count == 0)
                {
                    mongoDbLoader.MongoDbSeed();
                    orders = mongoDbLoader.retrieveData();
                }

                foreach (var order in orders)
                {
                    var mergedOrder = MergeWithExistingData(order, context);

                    context.Orders.Add(mergedOrder);
                }

                context.SaveChanges();
            }
            catch (MongoConnectionException e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private static Order MergeWithExistingData(Order order, ICoffeeCompanyDbContext context)
        {
            var mergedOrder = new Order
                {
                    ClientCompany = MergeClientCompany(order.ClientCompany, context),
                    QuantityInKg = order.QuantityInKg,
                    Status = order.Status,
                    Products = new HashSet<Product>()
                };

            foreach(var product in order.Products)
            {
                Product mergedProduct = MergeProduct(product, context);
                mergedOrder.Products.Add(mergedProduct);
            }

            return mergedOrder;
        }

        private static ClientCompany MergeClientCompany(ClientCompany clientCompany, ICoffeeCompanyDbContext context)
        {
            var mergedClientCompany = context.Companies.Where(c => c.Name == clientCompany.Name).FirstOrDefault();

            if (mergedClientCompany != null)
            {
                return mergedClientCompany;
            }

            return clientCompany;
        }

        private static Product MergeProduct(Product product, ICoffeeCompanyDbContext context)
        {
            var mergedProduct = context.Products.Where(p => p.Name == product.Name).FirstOrDefault();

            if (mergedProduct != null)
            {
                return mergedProduct;
            }

            return product;
        }

        public static void ImportFromExcel(
            string zipToUnpack = DefaultZipToUnpack,
            string unpackDirectory = DefaultUnpackDirectory)
        {
            var context = new CoffeeCompanyDbContext();

            try
            {
                var excelLoader = new ExcelLoader(zipToUnpack, unpackDirectory);

                var companies = excelLoader.retrieveCompaniesData();
                var products = excelLoader.retrieveProductData();

                foreach (var company in companies)
                {
                    if (context.Companies.Any(c => c.Name == company.Name))
                    {
                        continue;
                    }

                    context.Companies.Add(company);
                }

                foreach (var product in products)
                {
                    if (context.Products.Any(p => p.Name == product.Name))
                    {
                        continue;
                    }

                    context.Products.Add(product);
                }

                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public static void ImportFromXml(
            string xmlFilePath = DefaultXMLDataFileName)
        {
            var context = new CoffeeCompanyDbContext();

            try
            {
                var xmlLoader = new XmlLoader(xmlFilePath);

                var orders = xmlLoader.retrieveData();

                foreach (var order in orders)
                {
                    var mergedOrder = MergeWithExistingData(order, context);

                    context.Orders.Add(mergedOrder);
                }

                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
