namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewPropertiesProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Year", c => c.Int());
            AddColumn("dbo.Product", "IsNew", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "StateProvinceId", c => c.Int(nullable: false, defaultValue:1));
            CreateIndex("dbo.Product", "StateProvinceId");
            AddForeignKey("dbo.Product", "StateProvinceId", "dbo.StateProvince", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "StateProvinceId", "dbo.StateProvince");
            DropIndex("dbo.Product", new[] { "StateProvinceId" });
            DropColumn("dbo.Product", "StateProvinceId");
            DropColumn("dbo.Product", "IsNew");
            DropColumn("dbo.Product", "Year");
        }
    }
}
