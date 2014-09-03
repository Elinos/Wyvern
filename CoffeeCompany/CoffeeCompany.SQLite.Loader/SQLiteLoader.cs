using System;
using System.Collections.Generic;
using System.Linq;
using CoffeeCompany.SQLite.Data;
using CoffeeCompany.SQLite.Models;

namespace CoffeeCompany.SQLite.Loader
{
    public class SQLiteLoader
    {
        SQLiteContext sqLiteDb = new SQLiteContext();

        public void LoadData()
        {
            DeleteAllEntities("Discounts");
            
            CreateDiscountForCompany(5, 1);
            CreateDiscountForCompany(1, 2);
            CreateDiscountForCompany(2, 3);
            CreateDiscountForCompany(3, 4);

            sqLiteDb.SaveChanges();
        }

        private void DeleteAllEntities(string tableName)
        {
            var deleteAllEntitiesCommand = "DELETE FROM " + tableName + ";";
            sqLiteDb.Database.ExecuteSqlCommand(deleteAllEntitiesCommand);
            sqLiteDb.SaveChanges();
        }
        public void CreateDiscountForCompany(int companyID, int discountTypeID)
        {
            var discount = new Discount
            {
                CompanyId = companyID,
                DiscountTypeID = discountTypeID
            };
            sqLiteDb.Discounts.Add(discount);
            sqLiteDb.SaveChanges();
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
