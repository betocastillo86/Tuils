namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterColumn_Value_Table_Setting : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Setting", "Value", c => c.String(nullable: false, maxLength: 4000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Setting", "Value", c => c.String(nullable: false, maxLength: 2000));
        }
    }
}
