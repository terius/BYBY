namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1102 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBUserRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.TBUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TBRoles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DisplayName = c.String(),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.TBUsers", "UserName", c => c.String(nullable: false, maxLength: 20));
            AddColumn("dbo.TBUsers", "Password", c => c.String(nullable: false, maxLength: 32));
            AddColumn("dbo.TBUsers", "LastLoginTime", c => c.DateTime());
            AlterColumn("dbo.TBUsers", "Name", c => c.String(nullable: false, maxLength: 30));
            DropColumn("dbo.TBUsers", "SFZ");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TBUsers", "SFZ", c => c.String(nullable: false, maxLength: 18));
            DropForeignKey("dbo.TBUserRoles", "UserId", "dbo.TBUsers");
            DropForeignKey("dbo.TBUserRoles", "RoleId", "dbo.TBRoles");
            DropIndex("dbo.TBUserRoles", new[] { "RoleId" });
            DropIndex("dbo.TBUserRoles", new[] { "UserId" });
            AlterColumn("dbo.TBUsers", "Name", c => c.String(nullable: false, maxLength: 20));
            DropColumn("dbo.TBUsers", "LastLoginTime");
            DropColumn("dbo.TBUsers", "Password");
            DropColumn("dbo.TBUsers", "UserName");
            DropTable("dbo.TBRoles");
            DropTable("dbo.TBUserRoles");
        }
    }
}
