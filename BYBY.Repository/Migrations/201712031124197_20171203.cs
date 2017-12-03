namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20171203 : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.TBUserRoles", "RoleId", "dbo.TBRoles");
            //DropForeignKey("dbo.TBUserRoles", "UserId", "dbo.TBUsers");
            DropIndex("dbo.TBUserRoles", new[] { "UserId" });
            DropIndex("dbo.TBUserRoles", new[] { "RoleId" });
            //DropPrimaryKey("dbo.TBRoles");
            //DropPrimaryKey("dbo.TBUserRoles");
            //DropPrimaryKey("dbo.TBUsers");
            AlterColumn("dbo.TBRoles", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.TBUserRoles", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.TBUserRoles", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.TBUserRoles", "RoleId", c => c.String(maxLength: 128));
            AlterColumn("dbo.TBUsers", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.TBRoles", "Id");
            AddPrimaryKey("dbo.TBUserRoles", "Id");
            AddPrimaryKey("dbo.TBUsers", "Id");
            CreateIndex("dbo.TBUserRoles", "UserId");
            CreateIndex("dbo.TBUserRoles", "RoleId");
            AddForeignKey("dbo.TBUserRoles", "RoleId", "dbo.TBRoles", "Id");
            AddForeignKey("dbo.TBUserRoles", "UserId", "dbo.TBUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBUserRoles", "UserId", "dbo.TBUsers");
            DropForeignKey("dbo.TBUserRoles", "RoleId", "dbo.TBRoles");
            DropIndex("dbo.TBUserRoles", new[] { "RoleId" });
            DropIndex("dbo.TBUserRoles", new[] { "UserId" });
            DropPrimaryKey("dbo.TBUsers");
            DropPrimaryKey("dbo.TBUserRoles");
            DropPrimaryKey("dbo.TBRoles");
            AlterColumn("dbo.TBUsers", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.TBUserRoles", "RoleId", c => c.Int(nullable: false));
            AlterColumn("dbo.TBUserRoles", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.TBUserRoles", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.TBRoles", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.TBUsers", "Id");
            AddPrimaryKey("dbo.TBUserRoles", "Id");
            AddPrimaryKey("dbo.TBRoles", "Id");
            CreateIndex("dbo.TBUserRoles", "RoleId");
            CreateIndex("dbo.TBUserRoles", "UserId");
            AddForeignKey("dbo.TBUserRoles", "UserId", "dbo.TBUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.TBUserRoles", "RoleId", "dbo.TBRoles", "Id", cascadeDelete: true);
        }
    }
}
