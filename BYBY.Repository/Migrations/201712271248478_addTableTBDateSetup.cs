namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableTBDateSetup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBDateSetups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        STime = c.DateTime(nullable: false),
                        ETime = c.DateTime(nullable: false),
                        DefaultPeople = c.Int(nullable: false),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TBDateSetups");
        }
    }
}
