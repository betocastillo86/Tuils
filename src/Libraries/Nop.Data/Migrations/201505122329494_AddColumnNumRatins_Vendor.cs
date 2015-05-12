namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnNumRatins_Vendor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendor", "NumRatings", c => c.Int(nullable: false, defaultValue:0));
            CreateIndex("dbo.Product", "VendorId");
            AddForeignKey("dbo.Product", "VendorId", "dbo.Vendor", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "VendorId", "dbo.Vendor");
            DropIndex("dbo.Product", new[] { "VendorId" });
            DropColumn("dbo.Vendor", "NumRatings");
        }
    }
}
