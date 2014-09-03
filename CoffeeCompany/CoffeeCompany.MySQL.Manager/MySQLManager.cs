using System;
using System.Linq;
using CoffeeCompany.MySQL.Models;
using System.Collections.Generic;

namespace CoffeeCompany.MySQL.Manager
{
    public class MySQLManager
    {
        coffeecompanyreportsEntities mySQLDb = new coffeecompanyreportsEntities();

        public void AddReport(OrderInfo orderInfo)
        {
            var report = new Report
            {
                CompanyID = orderInfo.CompanyID,
                CompanyName = orderInfo.CompanyName,
                ProductName = orderInfo.ProductName,
                Price = orderInfo.Price,
                NumberOfOrders = orderInfo.NumberOfOrders,
                TotalRevenue = orderInfo.TotalRevenue
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
        public List<Report> GetAllReports()
        {
            var reports = mySQLDb.Reports1.ToList();
            return reports;
        }
    }
}
