namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201712042 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.TBUserRoles");
            AlterColumn("dbo.TBUserRoles", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.TBUserRoles", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.TBUserRoles");
            AlterColumn("dbo.TBUserRoles", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.TBUserRoles", "Id");
        }
    }
}
