namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_NumClicksForMoreInfo_Table_Product : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "NumClicksForMoreInfo", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "NumClicksForMoreInfo");
        }
    }
}
