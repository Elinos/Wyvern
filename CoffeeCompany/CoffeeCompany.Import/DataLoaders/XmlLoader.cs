namespace CoffeeCompany.Import.DataLoaders
{
    using System;
    using System.Collections.Generic;
    using System.Xml;

    using CoffeeCompany.Models;
    using CoffeeCompany.Import.DataLoaders.Contracts;

    internal class XmlLoader : IDataLoader
    {
        private string xmlFilePath;

        public XmlLoader(string xmlFilePath)
        {
            this.xmlFilePath = xmlFilePath;
        }

        public ICollection<Order> retrieveOrdersData()
        {
            ICollection<Order> orders = new HashSet<Order>();

            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(this.xmlFilePath);

                XmlNode rootNode = doc.DocumentElement;

                foreach (XmlNode node in rootNode.ChildNodes)
                {
                    orders.Add(OrderFromXMLBuilder(node));
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine("The file {0} doesn't exist");
                System.Console.WriteLine(e.Message);
            }

            return orders;
        }

        public ICollection<ClientCompany> retrieveCompaniesData()
        {
            throw new System.NotImplementedException();
        }

        public ICollection<Product> retrieveProductsData()
        {
            throw new System.NotImplementedException();
        }

        private Order OrderFromXMLBuilder(XmlNode node)
        {
            var company = new ClientCompany
            {
                Name = node["ClientCompany"]["Name"].InnerText,
                CountryOfOrigin = node["ClientCompany"]["CountryOfOrigin"].InnerText
            };

            var order = new Order
            {
                ClientCompany = company,
                Products = new HashSet<Product>(),
                QuantityInKg = int.Parse(node["QuantityInKg"].InnerText),
                Status = (OrderStatus)int.Parse(node["Status"].InnerText)
            };

            foreach (XmlNode rawProduct in node["Products"])
            {
                var product = new Product
                {
                    Name = rawProduct["Name"].InnerText,
                    PricePerKgInDollars = decimal.Parse(rawProduct["PricePerKgInDollars"].InnerText),
                    TypeOfCoffee = (CoffeeTypes)int.Parse(rawProduct["TypeOfCoffee"].InnerText)
                };

                order.Products.Add(product);
            }

            return order;
        }
    }
}
