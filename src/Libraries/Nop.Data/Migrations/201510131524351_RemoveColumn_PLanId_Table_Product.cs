namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveColumn_PLanId_Table_Product : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "PlanId", "dbo.Product");
            DropIndex("dbo.Product", new[] { "PlanId" });
            DropColumn("dbo.Product", "PlanId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Product", "PlanId", c => c.Int());
            CreateIndex("dbo.Product", "PlanId");
            AddForeignKey("dbo.Product", "PlanId", "dbo.Product", "Id");
        }
    }
}
