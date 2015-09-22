namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_HideTitle_Table_Topic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topic", "HideTitle", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topic", "HideTitle");
        }
    }
}
