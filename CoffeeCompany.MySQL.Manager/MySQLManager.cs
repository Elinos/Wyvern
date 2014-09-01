using System;
using System.Linq;
using CoffeeCompany.MySQL.Models;

namespace CoffeeCompany.MySQL.Manager
{
    public class MySQLManager
    {
        coffeecompanyreportsEntities mySQLDb = new coffeecompanyreportsEntities();

        public void AddReport(string productName, decimal price, int numberOfOrders, decimal totalRevenue)
        {
            var report = new Report
            {
                ProductName = productName,
                Price = price,
                NumberOfOrders = numberOfOrders,
                TotalRevenue = totalRevenue
            };
            mySQLDb.Reports1.Add(report);
            mySQLDb.SaveChanges();
        }

        public void DeleteReport(int reportID)
        {
            var reportToDelete = mySQLDb.Reports1.Find(reportID);
            mySQLDb.Reports1.Remove(reportToDelete);
            mySQLDb.SaveChanges();
        }
    }
}
