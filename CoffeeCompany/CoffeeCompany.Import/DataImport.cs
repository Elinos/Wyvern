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

        private const string DefaultZipToUnpack = @"..\..\..\..\dbData\ExcelReports.zip";
        private const string DefaultUnpackDirectory = @"..\..\..\..\dbData\ExcelReports";

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

        public bool ImportFromMongoDb(
            string connectionString = DefaultMongoDbConnectionString,
            string dbName = DefaultMongoDbName)
        {
            try
            {
                var mongoDbLoader = new MongoDbLoader(connectionString, dbName);

                //var orders = mongoDbLoader.retrieveOrdersData();
                var companies = mongoDbLoader.retrieveCompaniesData();
                var products = mongoDbLoader.retrieveProductsData();
                var employees = mongoDbLoader.retrieveEmployeesData();

                //Seed to mongodb database, if there're no data
                if (companies.Count == 0 && products.Count == 0 && employees.Count == 0)
                {
                    mongoDbLoader.MongoDbSeed();
                    companies = mongoDbLoader.retrieveCompaniesData();
                    products = mongoDbLoader.retrieveProductsData();
                    employees = mongoDbLoader.retrieveEmployeesData();
                }

                this.ImportCompanies(companies);
                this.ImportProducts(products);
                this.ImportEmployees(employees);

                return true;
            }
            catch (MongoConnectionException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool ImportFromExcel(
            string zipToUnpack = DefaultZipToUnpack,
            string unpackDirectory = DefaultUnpackDirectory)
        {
            try
            {
                var excelLoader = new ExcelLoader(zipToUnpack, unpackDirectory);

                var orders = excelLoader.retrieveOrdersData();

                ImportOrders(orders);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        public bool ImportFromXml(
            string xmlFilePath = DefaultXMLDataFileName)
        {
            try
            {
                var xmlLoader = new XmlLoader(xmlFilePath);

                var orders = xmlLoader.retrieveOrdersData();

                this.ImportOrders(orders);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
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

        private void ImportEmployees(ICollection<Employee> employees)
        {
            foreach (var employee in employees)
            {
                bool isExist;

                var mergedEmployee = this.MergeWithExistingEmployees(employee, out isExist);

                if (isExist)
                {
                    continue;
                }

                this.context.Employees.Add(mergedEmployee);
            }

            this.context.SaveChanges();
        }

        private void ImportOrders(ICollection<Order> orders)
        {
            foreach (var order in orders)
            {
                bool isExist;

                var mergedOrder = this.MergeWithExistingOrders(order, out isExist);

                if (isExist)
                {
                    continue;
                }

                this.context.Orders.Add(mergedOrder);
            }

            this.context.SaveChanges();
        }

        private Order MergeWithExistingOrders(Order order, out bool isExist)
        {
            var mergedOrder = this.context.Orders.Where(o => o.ClientCompany.Name == order.ClientCompany.Name &&
                                                    o.Employee.Username.ToLower().Trim() == order.Employee.Username.ToLower().Trim() &&
                                                    o.Status == order.Status &&
                                                    o.QuantityInKg == order.QuantityInKg).FirstOrDefault();
            if (mergedOrder != null)
            {
                isExist = true;
                return mergedOrder;
            }
            else
            {
                var mergedCompany = this.MergeWithExistingClientCompanies(order.ClientCompany);
                var mergedProduct = this.MergeWithExistingProducts(order.Product);
                var mergedEmployee = this.MergeWithExistingEmployees(order.Employee);

                mergedOrder = new Order
                {
                    ClientCompany = mergedCompany,
                    ClientCompanyId = mergedCompany.ID,
                    QuantityInKg = order.QuantityInKg,
                    Status = order.Status,
                    Product = mergedProduct,
                    Employee = mergedEmployee
                };

                isExist = false;
                return mergedOrder;
            }
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

        private Employee MergeWithExistingEmployees(Employee employee, out bool isExist)
        {
            var mergedEmployee = this.context.Employees
                                    .Where(e => e.Username.ToLower().Trim() == employee.Username.ToLower().Trim())
                                    .FirstOrDefault();

            if (mergedEmployee != null)
            {
                isExist = true;
                return mergedEmployee;
            }

            isExist = false;
            return employee;
        }

        private Employee MergeWithExistingEmployees(Employee employee)
        {
            var mergedEmployee = this.context.Employees
                                    .Where(e => e.Username.ToLower().Trim() == employee.Username.ToLower().Trim())
                                    .FirstOrDefault();

            if (mergedEmployee != null)
            {
                return mergedEmployee;
            }

            return employee;
        }
    }
}
