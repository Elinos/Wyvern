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

        private ICoffeeCompanyData context;

        public DataImport(ICoffeeCompanyData context)
        {
            this.context = context;
        }

        public DataImport()
            : this(new CoffeeCompanyData())
        {
        }

        public void ImportFromMongoDb(
            string connectionString = DefaultMongoDbConnectionString,
            string dbName = DefaultMongoDbName)
        {
            try
            {
                var mongoDbLoader = new MongoDbLoader(connectionString, dbName);

                //var orders = mongoDbLoader.retrieveOrdersData();
                var companies = mongoDbLoader.retrieveCompaniesData();
                var products = mongoDbLoader.retrieveProductsData();

                //Seed to mongodb database, if there're no data
                if (companies.Count == 0 && products.Count == 0)
                {
                    mongoDbLoader.MongoDbSeed();
                    companies = mongoDbLoader.retrieveCompaniesData();
                    products = mongoDbLoader.retrieveProductsData();
                }

                this.ImportCompanies(companies);
                this.ImportProducts(products);
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

                ImportCompanies(companies);
                ImportProducts(products);
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

                this.ImportOrders(orders);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        private void ImportCompanies(ICollection<ClientCompany> clientCompanies)
        {
            foreach (var company in clientCompanies)
            {
                if (this.context.ClientCompanies.Any(c => c.Name == company.Name))
                {
                    continue;
                }

                this.context.ClientCompanies.Add(company);
            }

            this.context.SaveChanges();
        }

        private void ImportProducts(ICollection<Product> products)
        {
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

        private void ImportOrders(ICollection<Order> orders)
        {
            foreach (var order in orders)
            {
                var mergedOrder = this.MergeWithExistingOrders(order);

                this.context.Orders.Add(mergedOrder);
            }

            this.context.SaveChanges();
        }

        private Order MergeWithExistingOrders(Order order)
        {
            var mergedCompany = this.MergeWithExistingClientCompanies(order.ClientCompany);

            var mergedOrder = new Order
                {
                    ClientCompany = mergedCompany,
                    ClientCompanyId = mergedCompany.ID,
                    QuantityInKg = order.QuantityInKg,
                    Status = order.Status,
                    Products = new HashSet<Product>()
                };

            foreach(var product in order.Products)
            {
                var mergedProduct = this.MergeWithExistingProducts(product);
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
