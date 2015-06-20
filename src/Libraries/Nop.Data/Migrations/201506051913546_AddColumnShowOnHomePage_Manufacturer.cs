namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnShowOnHomePage_Manufacturer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Manufacturer", "ShowOnHomePage", c => c.Boolean(nullable: false, defaultValue:false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Manufacturer", "ShowOnHomePage");
        }
    }
}
