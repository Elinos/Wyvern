namespace CoffeeCompany.Data
{
    using CoffeeCompany.Data.Migrations;
    using CoffeeCompany.Models;
    using System.Data.Entity;
    public class CoffeeCompanyDbContext : DbContext, ICoffeeCompanyDbContext
    {
        public CoffeeCompanyDbContext()
            : base("CoffeeCompanyConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CoffeeCompanyDbContext, Configuration>());
        }
        public IDbSet<ClientCompany> ClientCompanies { get; set; }

        public IDbSet<Product> Products { get; set; }

        public IDbSet<Order> Orders { get; set; }

        public IDbSet<Employee> Employees { get; set; }

        public new void SaveChanges()
        {
            base.SaveChanges();
        }
        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }
    }
}
