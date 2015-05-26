namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTable_ProductQuestion1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ProductQuestion", "Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductQuestion", "Status", c => c.Int(nullable: false));
        }
    }
}
