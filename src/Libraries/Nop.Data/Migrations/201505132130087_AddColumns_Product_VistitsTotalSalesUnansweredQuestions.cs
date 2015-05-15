namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumns_Product_VistitsTotalSalesUnansweredQuestions : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "Visits", c => c.Int(nullable: false, defaultValue:0));
            AddColumn("dbo.Product", "TotalSales", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.Product", "UnansweredQuestions", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "UnansweredQuestions");
            DropColumn("dbo.Product", "TotalSales");
            DropColumn("dbo.Product", "Visits");
        }
    }
}
