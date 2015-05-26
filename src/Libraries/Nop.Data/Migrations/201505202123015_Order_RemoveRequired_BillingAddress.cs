namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Order_RemoveRequired_BillingAddress : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Order", new[] { "BillingAddressId" });
            AlterColumn("dbo.Order", "BillingAddressId", c => c.Int());
            CreateIndex("dbo.Order", "BillingAddressId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Order", new[] { "BillingAddressId" });
            AlterColumn("dbo.Order", "BillingAddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.Order", "BillingAddressId");
        }
    }
}
