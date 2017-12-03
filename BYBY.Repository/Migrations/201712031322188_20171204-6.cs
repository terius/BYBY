namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201712046 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TBUserRoles", "RoleId", "dbo.TBRoles");
            DropForeignKey("dbo.TBUserRoles", "UserId", "dbo.TBUsers");
            DropPrimaryKey("dbo.TBRoles");
            DropPrimaryKey("dbo.TBUserRoles");
            DropPrimaryKey("dbo.TBUsers");
            AlterColumn("dbo.TBRoles", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.TBUserRoles", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.TBUsers", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.TBRoles", "Id");
            AddPrimaryKey("dbo.TBUserRoles", "Id");
            AddPrimaryKey("dbo.TBUsers", "Id");
            AddForeignKey("dbo.TBUserRoles", "RoleId", "dbo.TBRoles", "Id");
            AddForeignKey("dbo.TBUserRoles", "UserId", "dbo.TBUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBUserRoles", "UserId", "dbo.TBUsers");
            DropForeignKey("dbo.TBUserRoles", "RoleId", "dbo.TBRoles");
            DropPrimaryKey("dbo.TBUsers");
            DropPrimaryKey("dbo.TBUserRoles");
            DropPrimaryKey("dbo.TBRoles");
            AlterColumn("dbo.TBUsers", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.TBUserRoles", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.TBRoles", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.TBUsers", "Id");
            AddPrimaryKey("dbo.TBUserRoles", "Id");
            AddPrimaryKey("dbo.TBRoles", "Id");
            AddForeignKey("dbo.TBUserRoles", "UserId", "dbo.TBUsers", "Id");
            AddForeignKey("dbo.TBUserRoles", "RoleId", "dbo.TBRoles", "Id");
        }
    }
}
