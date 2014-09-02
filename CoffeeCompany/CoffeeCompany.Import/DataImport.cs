namespace CoffeeCompany.Import
{
    using System;
    using System.Collections.Generic;

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
                    context.Orders.Add(order);
                }

                context.SaveChanges();
            }
            catch (MongoConnectionException e)
            {
                Console.WriteLine(e.Message);
            }
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
                    context.Companies.Add(company);
                }

                foreach (var product in products)
                {
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
                    context.Orders.Add(order);
                }

                context.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
