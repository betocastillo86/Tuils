namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnRating_Order_OrderItemId_ProductReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductReview", "OrderItemId", c => c.Int(nullable: false, defaultValue:1));
            AddColumn("dbo.OrderItem", "Rating", c => c.Double());
            CreateIndex("dbo.ProductReview", "OrderItemId");
            AddForeignKey("dbo.ProductReview", "OrderItemId", "dbo.OrderItem", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductReview", "OrderItemId", "dbo.OrderItem");
            DropIndex("dbo.ProductReview", new[] { "OrderItemId" });
            DropColumn("dbo.OrderItem", "Rating");
            DropColumn("dbo.ProductReview", "OrderItemId");
        }
    }
}
