namespace CoffeeCompany.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using CoffeeCompany.Models;

    public interface ICoffeeCompanyDbContext
    {
        IDbSet<ClientCompany> Companies { get; set; }

        IDbSet<Product> Products { get; set; }

        IDbSet<Order> Orders { get; set; }

        IDbSet<T> Set<T>() where T : class;

        DbEntityEntry<T> Entry<T>(T entity) where T : class;

        void SaveChanges();
    }
}
