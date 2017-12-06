namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBModuleAddIsMenu : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBModules", "IsMenu", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBModules", "IsMenu");
        }
    }
}
