namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Active_Table_ProductPicture : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product_Picture_Mapping", "Active", c => c.Boolean(nullable: false, defaultValue:true));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product_Picture_Mapping", "Active");
        }
    }
}
