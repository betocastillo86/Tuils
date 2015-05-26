namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteColumnRating_OrderItem : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.OrderItem", "Rating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.OrderItem", "Rating", c => c.Double());
        }
    }
}
