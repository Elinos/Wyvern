using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeCompany.SQLite.Data;
using CoffeeCompany.SQLite.Models;

namespace CoffeeCompany.SQLite.Manager
{
    public class SQLiteManager
    {
        SQLiteContext sqLiteDb = new SQLiteContext();

        public void LoadData()
        {
            this.DeleteAllEntities("Discounts");
            
            this.CreateDiscountForCompany(5, 1);
            this.CreateDiscountForCompany(1, 2);
            this.CreateDiscountForCompany(2, 3);
            this.CreateDiscountForCompany(3, 4);

            this.sqLiteDb.SaveChanges();
        }

        private void DeleteAllEntities(string tableName)
        {
            var deleteAllEntitiesCommand = "DELETE FROM " + tableName + ";";
            this.sqLiteDb.Database.ExecuteSqlCommand(deleteAllEntitiesCommand);
            this.sqLiteDb.SaveChanges();
        }
        public void CreateDiscountForCompany(int companyID, int discountTypeID)
        {
            var discount = new Discount
            {
                CompanyId = companyID,
                DiscountTypeID = discountTypeID
            };

            this.sqLiteDb.Discounts.Add(discount);
            this.sqLiteDb.SaveChanges();
        }

        public List<DiscountInformation> GetDiscountPercentagesPerCompany()
        {
            var discountPercentages = from d in sqLiteDb.Discounts
                                      join dt in sqLiteDb.DiscountType on d.DiscountTypeID equals dt.DiscountTypeID
                                      select new DiscountInformation
                                      {
                                          CompanyID = d.CompanyId,
                                          DiscountPercent = dt.DiscountPercent
                                      };
            return discountPercentages.ToList();
        }
    }
}
