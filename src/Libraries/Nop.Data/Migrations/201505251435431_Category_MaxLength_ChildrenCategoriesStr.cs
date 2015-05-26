namespace Nop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category_MaxLength_ChildrenCategoriesStr : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Category", "ChildrenCategoriesStr", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Category", "ChildrenCategoriesStr", c => c.String(maxLength: 4000));
        }
    }
}
