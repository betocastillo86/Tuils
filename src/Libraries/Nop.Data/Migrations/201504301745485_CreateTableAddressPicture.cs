namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateTableAddressPicture : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address_Picture_Mapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressId = c.Int(nullable: false),
                        PictureId = c.Int(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Address", t => t.AddressId, cascadeDelete: true)
                .ForeignKey("dbo.Picture", t => t.PictureId, cascadeDelete: true)
                .Index(t => t.AddressId)
                .Index(t => t.PictureId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Address_Picture_Mapping", "PictureId", "dbo.Picture");
            DropForeignKey("dbo.Address_Picture_Mapping", "AddressId", "dbo.Address");
            DropIndex("dbo.Address_Picture_Mapping", new[] { "PictureId" });
            DropIndex("dbo.Address_Picture_Mapping", new[] { "AddressId" });
            DropTable("dbo.Address_Picture_Mapping");
        }
    }
}
