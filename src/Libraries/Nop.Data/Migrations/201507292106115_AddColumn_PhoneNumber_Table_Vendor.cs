namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_PhoneNumber_Table_Vendor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendor", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vendor", "PhoneNumber");
        }
    }
}
