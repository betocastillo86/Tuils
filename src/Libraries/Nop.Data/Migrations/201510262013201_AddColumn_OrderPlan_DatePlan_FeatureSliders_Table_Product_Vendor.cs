namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_OrderPlan_DatePlan_FeatureSliders_Table_Product_Vendor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "FeaturedForSliders", c => c.Boolean(nullable: false));
            AddColumn("dbo.Vendor", "CurrentOrderPlanId", c => c.Int());
            AddColumn("dbo.Vendor", "PlanExpiredOnUtc", c => c.DateTime());
            CreateIndex("dbo.Vendor", "CurrentOrderPlanId");
            AddForeignKey("dbo.Vendor", "CurrentOrderPlanId", "dbo.Order", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vendor", "CurrentOrderPlanId", "dbo.Order");
            DropIndex("dbo.Vendor", new[] { "CurrentOrderPlanId" });
            DropColumn("dbo.Vendor", "PlanExpiredOnUtc");
            DropColumn("dbo.Vendor", "CurrentOrderPlanId");
            DropColumn("dbo.Product", "FeaturedForSliders");
        }
    }
}
