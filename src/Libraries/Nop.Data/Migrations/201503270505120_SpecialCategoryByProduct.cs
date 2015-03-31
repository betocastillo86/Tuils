namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SpecialCategoryByProduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpecialCategoryProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        SpecialTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId)
                .ForeignKey("dbo.Category", t => t.CategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.ProductId);
            
           
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SpecialCategoryProduct", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.SpecialCategoryProduct", "ProductId", "dbo.Product");
        }
    }
}
