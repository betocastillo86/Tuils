namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumns_UserAgent_ProductName_TablePreproduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PreProduct", "ProductName", c => c.String(maxLength: 400));
            AddColumn("dbo.PreProduct", "UserAgent", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PreProduct", "UserAgent");
            DropColumn("dbo.PreProduct", "ProductName");
        }
    }
}
