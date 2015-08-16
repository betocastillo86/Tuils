namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_UnansweredQuestions_Table_Vendor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendor", "UnansweredQuestions", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vendor", "UnansweredQuestions");
        }
    }
}
