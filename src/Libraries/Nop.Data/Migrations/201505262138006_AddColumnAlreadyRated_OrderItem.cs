namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnAlreadyRated_OrderItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderItem", "AlreadyRated", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderItem", "AlreadyRated");
        }
    }
}
