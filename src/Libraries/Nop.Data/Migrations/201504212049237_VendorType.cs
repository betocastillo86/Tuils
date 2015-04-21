namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class VendorType : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendor", "VendorTypeId", c => c.Int(nullable: false, defaultValue:0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vendor", "VendorTypeId");
        }
    }
}
