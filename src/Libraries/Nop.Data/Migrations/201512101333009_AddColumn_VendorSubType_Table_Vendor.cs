namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_VendorSubType_Table_Vendor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendor", "VendorSubTypeId", c => c.Int(defaultValue:1, nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vendor", "VendorSubTypeId");
        }
    }
}
