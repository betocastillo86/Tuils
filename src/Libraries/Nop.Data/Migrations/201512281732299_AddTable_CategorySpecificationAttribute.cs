namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_CategorySpecificationAttribute : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Category_SpecificationAttribute_Mapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        Year = c.Int(nullable: false),
                        AttributeTypeId = c.Int(nullable: false),
                        SpecificationAttributeOptionId = c.Int(nullable: false),
                        CustomValue = c.String(maxLength: 4000),
                        AllowFiltering = c.Boolean(nullable: false),
                        ShowOnCategoryPage = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.SpecificationAttributeOption", t => t.SpecificationAttributeOptionId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.SpecificationAttributeOptionId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Category_SpecificationAttribute_Mapping", "SpecificationAttributeOptionId", "dbo.SpecificationAttributeOption");
            DropForeignKey("dbo.Category_SpecificationAttribute_Mapping", "CategoryId", "dbo.Category");
            DropIndex("dbo.Category_SpecificationAttribute_Mapping", new[] { "SpecificationAttributeOptionId" });
            DropIndex("dbo.Category_SpecificationAttribute_Mapping", new[] { "CategoryId" });
            DropTable("dbo.Category_SpecificationAttribute_Mapping");
        }
    }
}
