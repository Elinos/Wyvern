namespace CoffeeCompany.Data
{
    using CoffeeCompany.Data.Repositories;
    using CoffeeCompany.Models;
    using System.Data.Entity;
    public interface ICoffeeCompanyData
    {
        IGenericRepository<ClientCompany> ClientCompanies { get; }

        IGenericRepository<Order> Orders { get; }

        IGenericRepository<Product> Products { get; }

        IGenericRepository<Employee> Employees { get; }

        Database Database { get; }

        void SaveChanges();
    }
}
