namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _201712047 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TBUserRoles", "RoleId", "dbo.TBRoles");
            DropForeignKey("dbo.TBUserRoles", "UserId", "dbo.TBUsers");
            DropIndex("dbo.TBUserRoles", new[] { "UserId" });
            DropIndex("dbo.TBUserRoles", new[] { "RoleId" });
            AlterColumn("dbo.TBUserRoles", "UserId", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.TBUserRoles", "RoleId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.TBUserRoles", "UserId");
            CreateIndex("dbo.TBUserRoles", "RoleId");
            AddForeignKey("dbo.TBUserRoles", "RoleId", "dbo.TBRoles", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TBUserRoles", "UserId", "dbo.TBUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBUserRoles", "UserId", "dbo.TBUsers");
            DropForeignKey("dbo.TBUserRoles", "RoleId", "dbo.TBRoles");
            DropIndex("dbo.TBUserRoles", new[] { "RoleId" });
            DropIndex("dbo.TBUserRoles", new[] { "UserId" });
            AlterColumn("dbo.TBUserRoles", "RoleId", c => c.String(maxLength: 128));
            AlterColumn("dbo.TBUserRoles", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.TBUserRoles", "RoleId");
            CreateIndex("dbo.TBUserRoles", "UserId");
            AddForeignKey("dbo.TBUserRoles", "UserId", "dbo.TBUsers", "Id");
            AddForeignKey("dbo.TBUserRoles", "RoleId", "dbo.TBRoles", "Id");
        }
    }
}
