namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBModuleAddIcon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBModules", "Icon", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBModules", "Icon");
        }
    }
}
