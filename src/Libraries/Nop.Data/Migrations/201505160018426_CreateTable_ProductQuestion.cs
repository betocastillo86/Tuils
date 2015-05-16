namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTable_ProductQuestion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductQuestion",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        QuestionText = c.String(maxLength: 400),
                        StatusId = c.Int(nullable: false),
                        CustomerAnswerId = c.Int(),
                        AnswerText = c.String(maxLength: 400, nullable:true),
                        CreatedOnUtc = c.DateTime(nullable: false),
                        AnsweredOnUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Customer", t => t.CustomerAnswerId)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerId)
                .Index(t => t.CustomerAnswerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductQuestion", "ProductId", "dbo.Product");
            DropForeignKey("dbo.ProductQuestion", "CustomerAnswerId", "dbo.Customer");
            DropForeignKey("dbo.ProductQuestion", "CustomerId", "dbo.Customer");
            DropIndex("dbo.ProductQuestion", new[] { "CustomerAnswerId" });
            DropIndex("dbo.ProductQuestion", new[] { "CustomerId" });
            DropIndex("dbo.ProductQuestion", new[] { "ProductId" });
            DropTable("dbo.ProductQuestion");
        }
    }
}
