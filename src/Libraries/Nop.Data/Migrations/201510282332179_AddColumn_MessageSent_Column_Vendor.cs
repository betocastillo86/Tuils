namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_MessageSent_Column_Vendor : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vendor", "ExpirationPlanMessageSent", c => c.Boolean(nullable: false));
            AddColumn("dbo.Vendor", "PlanFinishedMessageSent", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Vendor", "PlanFinishedMessageSent");
            DropColumn("dbo.Vendor", "ExpirationPlanMessageSent");
        }
    }
}
