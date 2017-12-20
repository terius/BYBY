namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableTBReferral : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBReferrals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TBMedicalHistoryId = c.Int(nullable: false),
                        HospitalId = c.Int(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                        Remark = c.String(maxLength: 500),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBHospitals", t => t.HospitalId, cascadeDelete: true)
                .ForeignKey("dbo.TBMedicalHistories", t => t.TBMedicalHistoryId, cascadeDelete: true)
                .Index(t => t.TBMedicalHistoryId)
                .Index(t => t.HospitalId);
            
            AlterColumn("dbo.TBConsultations", "Remark", c => c.String(maxLength: 500));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBReferrals", "TBMedicalHistoryId", "dbo.TBMedicalHistories");
            DropForeignKey("dbo.TBReferrals", "HospitalId", "dbo.TBHospitals");
            DropIndex("dbo.TBReferrals", new[] { "HospitalId" });
            DropIndex("dbo.TBReferrals", new[] { "TBMedicalHistoryId" });
            AlterColumn("dbo.TBConsultations", "Remark", c => c.String(maxLength: 200));
            DropTable("dbo.TBReferrals");
        }
    }
}
