namespace CoffeeCompany.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CoffeeCompanyDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "CoffeeCompany.Data.CoffeeCompanyDbContext";
        }

        protected override void Seed(CoffeeCompanyDbContext context)
        {

        }
    }
}
