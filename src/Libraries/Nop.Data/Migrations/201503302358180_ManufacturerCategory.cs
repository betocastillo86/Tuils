namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManufacturerCategory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Manufacturer_Category_Mapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ManufacturerId = c.Int(nullable: false),
                        CategoryId = c.Int(nullable: false),
                        IsFeaturedManufacturer = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Manufacturer", t => t.ManufacturerId)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .Index(t => t.ManufacturerId)
                .Index(t => t.CategoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Manufacturer_Category_Mapping", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Manufacturer_Category_Mapping", "ManufacturerId", "dbo.Manufacturer");
            DropIndex("dbo.Manufacturer_Category_Mapping", new[] { "CategoryId" });
            DropIndex("dbo.Manufacturer_Category_Mapping", new[] { "ManufacturerId" });
            DropTable("dbo.Manufacturer_Category_Mapping");
        }
    }
}
