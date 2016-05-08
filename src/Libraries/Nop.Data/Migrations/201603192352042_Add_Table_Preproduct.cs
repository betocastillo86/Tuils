namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Table_Preproduct : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PreProduct",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(nullable: false),
                        JsonObject = c.String(nullable: false),
                        ProductTypeId = c.Int(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false),
                        UpdatedOnUtc = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PreProduct", "CustomerId", "dbo.Customer");
            DropIndex("dbo.PreProduct", new[] { "CustomerId" });
            DropTable("dbo.PreProduct");
        }
    }
}
