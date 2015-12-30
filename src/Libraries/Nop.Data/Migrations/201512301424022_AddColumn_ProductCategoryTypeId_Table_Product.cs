namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_ProductCategoryTypeId_Table_Product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ProductCategoryTypeId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "ProductCategoryTypeId");
        }
    }
}
