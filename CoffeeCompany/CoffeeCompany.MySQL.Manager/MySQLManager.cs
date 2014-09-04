using System;
using System.Linq;
using CoffeeCompany.MySQL.Models;
using System.Collections.Generic;
using CoffeeCompany.ReportGenerator;
using CoffeeCompany.Data;

namespace CoffeeCompany.MySQL.Manager
{
    public class MySQLManager
    {
        coffeecompanyreportsEntities mySQLDb = new coffeecompanyreportsEntities();

        public void AddReports(List<OrderInfo> orderInfos)
        {
            foreach (var orderInfo in orderInfos)
            {
                var report = new Report
                {
                    CompanyID = orderInfo.CompanyId,
                    CompanyName = orderInfo.CompanyName,
                    ProductName = orderInfo.ProductName,
                    Price = orderInfo.ProductPrice,
                    Quantity = orderInfo.Quantity,
                    TotalRevenue = orderInfo.RevenueFromOrder
                };

                this.mySQLDb.Reports1.Add(report);
            }

            this.mySQLDb.SaveChanges();
        }

        public void DeleteReport(int reportID)
        {
            var reportToDelete = this.mySQLDb.Reports1.Find(reportID);
            this.mySQLDb.Reports1.Remove(reportToDelete);
            this.mySQLDb.SaveChanges();
        }
        public List<Report> GetAllReports()
        {
            var reports = this.mySQLDb.Reports1.ToList();
            return reports;
        }

        public void ClearMySqlDb()
        {
            this.mySQLDb.Database.ExecuteSqlCommand("TRUNCATE TABLE reports");
        }

        public bool LoadAllReportsDataFromSQLServer()
        {
            try
            {
                var reportEngine = new ReportsEngine(new CoffeeCompanyData());
                var reportInfo = reportEngine.GetOrderInfo();
                this.AddReports(reportInfo);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
