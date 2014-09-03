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
            DeleteAllEntities(sqLiteDb, "Discounts");
            
            CreateDiscountForCompany(sqLiteDb, 5, 1);
            CreateDiscountForCompany(sqLiteDb, 1, 2);
            CreateDiscountForCompany(sqLiteDb, 2, 3);
            CreateDiscountForCompany(sqLiteDb, 3, 4);

            sqLiteDb.SaveChanges();
        }

        private void DeleteAllEntities(SQLiteContext context, string tableName)
        {
            var deleteAllEntitiesCommand = "DELETE FROM " + tableName + ";";
            context.Database.ExecuteSqlCommand(deleteAllEntitiesCommand);
            context.SaveChanges();
        }
        public void CreateDiscountForCompany(SQLiteContext db, int companyID, int discountTypeID)
        {
            var discount = new Discount
            {
                CompanyId = companyID,
                DiscountTypeID = discountTypeID
            };
            db.Discounts.Add(discount);
            db.SaveChanges();
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
