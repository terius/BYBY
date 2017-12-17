namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTableTBMedicalHistoryImage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBMedicalHistoryImages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 30),
                        FilePath = c.String(nullable: false, maxLength: 100),
                        TBMedicalHistoryId = c.Int(nullable: false),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBMedicalHistories", t => t.TBMedicalHistoryId, cascadeDelete: true)
                .Index(t => t.TBMedicalHistoryId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBMedicalHistoryImages", "TBMedicalHistoryId", "dbo.TBMedicalHistories");
            DropIndex("dbo.TBMedicalHistoryImages", new[] { "TBMedicalHistoryId" });
            DropTable("dbo.TBMedicalHistoryImages");
        }
    }
}
