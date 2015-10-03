namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Sold_Table_Product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Sold", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "Sold");
        }
    }
}
