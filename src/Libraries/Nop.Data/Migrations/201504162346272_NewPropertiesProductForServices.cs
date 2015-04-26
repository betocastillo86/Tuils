namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewPropertiesProductForServices : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "DetailShipping", c => c.String(maxLength: 500));
            AddColumn("dbo.Product", "IncludeSupplies", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "SuppliesValue", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "SuppliesValue");
            DropColumn("dbo.Product", "IncludeSupplies");
            DropColumn("dbo.Product", "DetailShipping");
        }
    }
}
