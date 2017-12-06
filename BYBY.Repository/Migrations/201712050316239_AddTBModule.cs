namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTBModule : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBModules",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 30),
                        Text = c.String(nullable: false, maxLength: 50),
                        Path = c.String(maxLength: 100),
                        ParentId = c.String(maxLength: 128),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBModules", t => t.ParentId)
                .Index(t => t.ParentId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBModules", "ParentId", "dbo.TBModules");
            DropIndex("dbo.TBModules", new[] { "ParentId" });
            DropTable("dbo.TBModules");
        }
    }
}
