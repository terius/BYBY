namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBModuleAddOrderBy : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBModules", "OrderBy", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBModules", "OrderBy");
        }
    }
}
