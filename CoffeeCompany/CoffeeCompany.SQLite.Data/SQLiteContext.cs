using System;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using CoffeeCompany.SQLite.Models;

namespace CoffeeCompany.SQLite.Data
{
    public class SQLiteContext : DbContext
    {
        const string ConnectionString = @"Data Source=..\..\..\CoffeeCompany.SQLite.Data\sqlitedb;";

        public SQLiteContext()
            : base(new SQLiteConnection() { ConnectionString = ConnectionString }, true)
        {
        }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<DiscountType> DiscountType { get; set; }

    }
}
