namespace CoffeeCompany.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClientCompanies",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        CountryOfOrigin = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        QuantityInKg = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                        ClientCompany_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ClientCompanies", t => t.ClientCompany_ID)
                .Index(t => t.ClientCompany_ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        PricePerKgInDollars = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TypeOfCoffee = c.Int(nullable: false),
                        Order_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.Order_ID)
                .Index(t => t.Order_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ClientCompany_ID", "dbo.ClientCompanies");
            DropForeignKey("dbo.Products", "Order_ID", "dbo.Orders");
            DropIndex("dbo.Products", new[] { "Order_ID" });
            DropIndex("dbo.Orders", new[] { "ClientCompany_ID" });
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.ClientCompanies");
        }
    }
}
