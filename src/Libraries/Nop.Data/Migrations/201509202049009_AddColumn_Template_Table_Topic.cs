namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_Template_Table_Topic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topic", "TemplateName", c => c.String());
            AddColumn("dbo.Topic", "FullWidth", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topic", "FullWidth");
            DropColumn("dbo.Topic", "TemplateName");
        }
    }
}
