namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumn_ChildrenCategoriesStr_Category : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Category", "ChildrenCategoriesStr", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Category", "ChildrenCategoriesStr");
        }
    }
}
