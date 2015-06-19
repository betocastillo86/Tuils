namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_LeftFeatured_Table_Product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "LeftFeatured", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "LeftFeatured");
        }
    }
}
