namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_SpecificationAttributeOptionId_Category : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "SpecificationAttributeOptionId", c => c.Int());
            CreateIndex("dbo.Category", "SpecificationAttributeOptionId");
            AddForeignKey("dbo.Category", "SpecificationAttributeOptionId", "dbo.SpecificationAttributeOption", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Category", "SpecificationAttributeOptionId", "dbo.SpecificationAttributeOption");
            DropIndex("dbo.Category", new[] { "SpecificationAttributeOptionId" });
            DropColumn("dbo.Category", "SpecificationAttributeOptionId");
        }
    }
}
