namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_ScheduledOnUtc_Table_QueueEmail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.QueuedEmail", "ScheduledOnUtc", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.QueuedEmail", "ScheduledOnUtc");
        }
    }
}
