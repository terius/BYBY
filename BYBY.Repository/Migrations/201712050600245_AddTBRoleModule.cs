namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTBRoleModule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBRoleModules",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        ModuleId = c.String(nullable: false, maxLength: 128),
                        OrderBy = c.Int(),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBModules", t => t.ModuleId, cascadeDelete: true)
                .ForeignKey("dbo.TBRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.RoleId)
                .Index(t => t.ModuleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBRoleModules", "RoleId", "dbo.TBRoles");
            DropForeignKey("dbo.TBRoleModules", "ModuleId", "dbo.TBModules");
            DropIndex("dbo.TBRoleModules", new[] { "ModuleId" });
            DropIndex("dbo.TBRoleModules", new[] { "RoleId" });
            DropTable("dbo.TBRoleModules");
        }
    }
}
