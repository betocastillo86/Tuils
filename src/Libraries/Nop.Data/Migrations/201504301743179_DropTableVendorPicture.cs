namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropTableVendorPicture : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vendor_Picture_Mapping", "PictureId", "dbo.Picture");
            DropForeignKey("dbo.Vendor_Picture_Mapping", "VendorId", "dbo.Vendor");
            DropIndex("dbo.Vendor_Picture_Mapping", new[] { "VendorId" });
            DropIndex("dbo.Vendor_Picture_Mapping", new[] { "PictureId" });
            DropTable("dbo.Vendor_Picture_Mapping");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Vendor_Picture_Mapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        VendorId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.Vendor_Picture_Mapping", "PictureId");
            CreateIndex("dbo.Vendor_Picture_Mapping", "VendorId");
            AddForeignKey("dbo.Vendor_Picture_Mapping", "VendorId", "dbo.Vendor", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Vendor_Picture_Mapping", "PictureId", "dbo.Picture", "Id", cascadeDelete: true);
        }
    }
}
