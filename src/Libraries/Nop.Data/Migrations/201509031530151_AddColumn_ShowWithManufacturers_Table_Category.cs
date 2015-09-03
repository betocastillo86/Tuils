namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_ShowWithManufacturers_Table_Category : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "ShowWithManufacturers", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "ShowWithManufacturers");
        }
    }
}
