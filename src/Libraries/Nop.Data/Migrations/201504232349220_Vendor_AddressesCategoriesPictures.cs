namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Vendor_AddressesCategoriesPictures : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SpecialCategoryVendor",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CategoryId = c.Int(nullable: false),
                        VendorId = c.Int(nullable: false),
                        SpecialTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Category", t => t.CategoryId, cascadeDelete: true)
                .ForeignKey("dbo.Vendor", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.VendorId);
            
            CreateTable(
                "dbo.Vendor_Picture_Mapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VendorId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Picture", t => t.PictureId, cascadeDelete: true)
                .ForeignKey("dbo.Vendor", t => t.VendorId, cascadeDelete: true)
                .Index(t => t.VendorId)
                .Index(t => t.PictureId);
            
            AddColumn("dbo.Address", "Longitude", c => c.Double());
            AddColumn("dbo.Address", "Latitude", c => c.Double());
            AddColumn("dbo.Address", "Schedule", c => c.String(maxLength: 100));
            AddColumn("dbo.Address", "Active", c => c.Boolean(nullable: false));
            AddColumn("dbo.Address", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Address", "VendorId", c => c.Int());
            AddColumn("dbo.Address", "DisplayOrder", c => c.Int(nullable: false));
            AddColumn("dbo.Vendor", "EnableCreditCardPayment", c => c.Boolean());
            AddColumn("dbo.Vendor", "EnableShipping", c => c.Boolean());
            AddColumn("dbo.Vendor", "PictureId", c => c.Int());
            AddColumn("dbo.Vendor", "BackgroundPictureId", c => c.Int());
            AddColumn("dbo.Vendor", "AvgRating", c => c.Double());
            CreateIndex("dbo.Vendor", "PictureId");
            CreateIndex("dbo.Vendor", "BackgroundPictureId");
            CreateIndex("dbo.Address", "VendorId");
            AddForeignKey("dbo.Address", "VendorId", "dbo.Vendor", "Id");
            AddForeignKey("dbo.Vendor", "BackgroundPictureId", "dbo.Picture", "Id");
            AddForeignKey("dbo.Vendor", "PictureId", "dbo.Picture", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vendor_Picture_Mapping", "VendorId", "dbo.Vendor");
            DropForeignKey("dbo.Vendor_Picture_Mapping", "PictureId", "dbo.Picture");
            DropForeignKey("dbo.SpecialCategoryVendor", "VendorId", "dbo.Vendor");
            DropForeignKey("dbo.SpecialCategoryVendor", "CategoryId", "dbo.Category");
            DropForeignKey("dbo.Vendor", "PictureId", "dbo.Picture");
            DropForeignKey("dbo.Vendor", "BackgroundPictureId", "dbo.Picture");
            DropForeignKey("dbo.Address", "VendorId", "dbo.Vendor");
            DropIndex("dbo.Vendor_Picture_Mapping", new[] { "PictureId" });
            DropIndex("dbo.Vendor_Picture_Mapping", new[] { "VendorId" });
            DropIndex("dbo.SpecialCategoryVendor", new[] { "VendorId" });
            DropIndex("dbo.SpecialCategoryVendor", new[] { "CategoryId" });
            DropIndex("dbo.Address", new[] { "VendorId" });
            DropIndex("dbo.Vendor", new[] { "BackgroundPictureId" });
            DropIndex("dbo.Vendor", new[] { "PictureId" });
            DropColumn("dbo.Vendor", "AvgRating");
            DropColumn("dbo.Vendor", "BackgroundPictureId");
            DropColumn("dbo.Vendor", "PictureId");
            DropColumn("dbo.Vendor", "EnableShipping");
            DropColumn("dbo.Vendor", "EnableCreditCardPayment");
            DropColumn("dbo.Address", "DisplayOrder");
            DropColumn("dbo.Address", "VendorId");
            DropColumn("dbo.Address", "Deleted");
            DropColumn("dbo.Address", "Active");
            DropColumn("dbo.Address", "Schedule");
            DropColumn("dbo.Address", "Latitude");
            DropColumn("dbo.Address", "Longitude");
            DropTable("dbo.Vendor_Picture_Mapping");
            DropTable("dbo.SpecialCategoryVendor");
        }
    }
}
