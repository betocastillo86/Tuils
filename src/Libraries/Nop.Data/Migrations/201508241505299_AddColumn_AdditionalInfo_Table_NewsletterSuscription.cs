namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_AdditionalInfo_Table_NewsletterSuscription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.NewsLetterSubscription", "AdditionalInfo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.NewsLetterSubscription", "AdditionalInfo");
        }
    }
}
