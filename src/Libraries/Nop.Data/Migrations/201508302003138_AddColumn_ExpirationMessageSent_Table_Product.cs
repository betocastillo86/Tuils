namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_ExpirationMessageSent_Table_Product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "ExpirationMessageSent", c => c.Boolean(nullable: false));
            AddColumn("dbo.Product", "PublishingFinishedMessageSent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "PublishingFinishedMessageSent");
            DropColumn("dbo.Product", "ExpirationMessageSent");
        }
    }
}
