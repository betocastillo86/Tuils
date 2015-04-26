namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NewslettersNewFields_Type : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsLetterSubscription", "SuscriptionTypeId", c => c.Int(nullable: false, defaultValue:1));
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewsLetterSubscription", "SuscriptionTypeId");
        }
    }
}
