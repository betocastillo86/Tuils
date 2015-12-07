namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_PlanExpirationOnUtc_PlanStartOnUtc_Table_Order : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Order", "PlanStartOnUtc", c => c.DateTime());
            AddColumn("dbo.Order", "PlanExpirationOnUtc", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Order", "PlanExpirationOnUtc");
            DropColumn("dbo.Order", "PlanStartOnUtc");
        }
    }
}
