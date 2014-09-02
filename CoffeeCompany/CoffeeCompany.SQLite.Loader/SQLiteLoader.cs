using System;
using System.Linq;
using CoffeeCompany.SQLite.Models;
using CoffeeCompany.SQLite.Data;

namespace CoffeeCompany.SQLite.Loader
{
    public class SQLiteLoader
    {

        public void LoadData()
        {
            var sqLiteDb = new SQLiteContext();
            DeleteAllEntities(sqLiteDb, "Discounts");
            DeleteAllEntities(sqLiteDb, "DiscountTypes");
            
            sqLiteDb.DiscountType.Add(new DiscountType { Name = "Regular", DiscountPercent = 0 });
            sqLiteDb.DiscountType.Add(new DiscountType { Name = "Silver", DiscountPercent = 2 });
            sqLiteDb.DiscountType.Add(new DiscountType { Name = "Gold", DiscountPercent = 4 });
            sqLiteDb.DiscountType.Add(new DiscountType { Name = "Platinum", DiscountPercent = 6 });
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
        private void CreateDiscountForCompany(SQLiteContext db, int companyID, int discountTypeID)
        {
            var discount = new Discount
            {
                CompanyId = companyID,
                DiscountTypeID = discountTypeID
            };
            db.Discounts.Add(discount);
            db.SaveChanges();
        }
    }
}
