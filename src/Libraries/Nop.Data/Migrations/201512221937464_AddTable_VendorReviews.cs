namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTable_VendorReviews : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.VendorReview",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        VendorId = c.Int(nullable: false),
                        IsApproved = c.Boolean(nullable: false),
                        Title = c.String(),
                        ReviewText = c.String(),
                        Rating = c.Int(nullable: false),
                        HelpfulYesTotal = c.Int(nullable: false),
                        HelpfulNoTotal = c.Int(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Vendor", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.CustomerId)
                .Index(t => t.VendorId);
            
            CreateTable(
                "dbo.VendorReviewHelpfulness",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VendorReviewId = c.Int(nullable: false),
                        WasHelpful = c.Boolean(nullable: false),
                        CustomerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.VendorReview", t => t.VendorReviewId, cascadeDelete: true)
                .Index(t => t.VendorReviewId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VendorReviewHelpfulness", "VendorReviewId", "dbo.VendorReview");
            DropForeignKey("dbo.VendorReview", "VendorId", "dbo.Vendor");
            DropForeignKey("dbo.VendorReview", "CustomerId", "dbo.Customer");
            DropIndex("dbo.VendorReviewHelpfulness", new[] { "VendorReviewId" });
            DropIndex("dbo.VendorReview", new[] { "VendorId" });
            DropIndex("dbo.VendorReview", new[] { "CustomerId" });
            DropTable("dbo.VendorReviewHelpfulness");
            DropTable("dbo.VendorReview");
        }
    }
}
