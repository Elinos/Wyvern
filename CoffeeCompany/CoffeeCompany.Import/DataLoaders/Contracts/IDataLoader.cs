namespace CoffeeCompany.Import.DataLoaders.Contracts
{
    using System.Collections.Generic;
    using CoffeeCompany.Models;

    interface IDataLoader
    {
        ICollection<Order> retrieveOrdersData();
        ICollection<ClientCompany> retrieveCompaniesData();
        ICollection<Product> retrieveProductsData();

        ICollection<Employee> retrieveEmployeesData();
    }
}
