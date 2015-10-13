namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_PlanIdHiddenStateProvinceId_Table_Product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Hidden", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "PlanId", c => c.Int());
            AlterColumn("dbo.Product", "StateProvinceId", c => c.Int(nullable: true));
            CreateIndex("dbo.Product", "PlanId");
            AddForeignKey("dbo.Product", "PlanId", "dbo.Product", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "PlanId", "dbo.Product");
            DropIndex("dbo.Product", new[] { "PlanId" });
            DropColumn("dbo.Product", "PlanId");
            DropColumn("dbo.Product", "Hidden");
        }
    }
}
