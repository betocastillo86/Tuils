namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnVendor_BackgroundPosition : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendor", "BackgroundPosition", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vendor", "BackgroundPosition");
        }
    }
}
