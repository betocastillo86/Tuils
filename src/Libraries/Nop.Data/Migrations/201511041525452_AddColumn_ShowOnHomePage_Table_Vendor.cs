namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_ShowOnHomePage_Table_Vendor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendor", "ShowOnHomePage", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vendor", "ShowOnHomePage");
        }
    }
}
