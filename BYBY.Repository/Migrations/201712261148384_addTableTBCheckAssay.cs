namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableTBCheckAssay : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBCheckAssays",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 100),
                        Name = c.String(nullable: false, maxLength: 100),
                        AssayType = c.Int(),
                        ParentItem = c.Int(),
                        EffectiveTime = c.Int(),
                        CheckMode = c.Int(),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TBConsultationChecks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CheckId = c.Int(nullable: false),
                        ConsultationId = c.Int(nullable: false),
                        ActionDate = c.DateTime(nullable: false),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBCheckAssays", t => t.CheckId, cascadeDelete: true)
                .ForeignKey("dbo.TBConsultations", t => t.ConsultationId, cascadeDelete: true)
                .Index(t => t.CheckId)
                .Index(t => t.ConsultationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBConsultationChecks", "ConsultationId", "dbo.TBConsultations");
            DropForeignKey("dbo.TBConsultationChecks", "CheckId", "dbo.TBCheckAssays");
            DropIndex("dbo.TBConsultationChecks", new[] { "ConsultationId" });
            DropIndex("dbo.TBConsultationChecks", new[] { "CheckId" });
            DropTable("dbo.TBConsultationChecks");
            DropTable("dbo.TBCheckAssays");
        }
    }
}
