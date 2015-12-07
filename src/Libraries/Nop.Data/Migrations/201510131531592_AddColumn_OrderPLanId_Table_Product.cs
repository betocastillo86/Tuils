namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_OrderPLanId_Table_Product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "OrderPlanId", c => c.Int());
            CreateIndex("dbo.Product", "OrderPlanId");
            AddForeignKey("dbo.Product", "OrderPlanId", "dbo.Order", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "OrderPlanId", "dbo.Order");
            DropIndex("dbo.Product", new[] { "OrderPlanId" });
            DropColumn("dbo.Product", "OrderPlanId");
        }
    }
}
