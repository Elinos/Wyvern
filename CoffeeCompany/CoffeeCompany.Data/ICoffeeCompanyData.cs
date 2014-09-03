namespace CoffeeCompany.Data
{
    using CoffeeCompany.Data.Repositories;
    using CoffeeCompany.Models;
    public interface ICoffeeCompanyData
    {
        IGenericRepository<ClientCompany> ClientCompanies { get; }

        IGenericRepository<Order> Orders { get; }

        IGenericRepository<Product> Products { get; }

        void SaveChanges();
    }
}
