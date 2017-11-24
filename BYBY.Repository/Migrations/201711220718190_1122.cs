namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1122 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBMedicalDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        CurrentInfoMain = c.String(maxLength: 100),
                        CurrentInfo = c.String(maxLength: 100),
                        PastHepatitis = c.String(maxLength: 100),
                        PastSurgery = c.String(maxLength: 100),
                        PastTuberculosis = c.String(maxLength: 100),
                        PastUrinarySystemDisease = c.String(maxLength: 100),
                        PastCardiovascularDisease = c.String(maxLength: 100),
                        PastPelvicInflammatoryDisease = c.String(maxLength: 100),
                        PastSTD = c.String(maxLength: 100),
                        PastKidneyDisease = c.String(maxLength: 100),
                        PastOther = c.String(maxLength: 200),
                        MenstruationFirstAge = c.Int(),
                        MenstruationCycle = c.Int(),
                        MenstruationDuration = c.Int(),
                        MenstruationVolume = c.String(maxLength: 20),
                        MenstruationDysmenorrhea = c.String(maxLength: 100),
                        MenstruationGore = c.String(maxLength: 100),
                        MenstruationLast = c.DateTime(),
                        MarriageRelatives = c.String(maxLength: 100),
                        MarriageRemarry = c.String(maxLength: 100),
                        MarriageLastPregnancyDate = c.DateTime(),
                        MarriageChildren = c.String(maxLength: 100),
                        MarriageFertility = c.String(maxLength: 200),
                        MarriageEctopicPregnancy = c.Int(),
                        MarriageSurgeryAndDate = c.String(maxLength: 200),
                        PhysiqueHeight = c.Int(nullable: false),
                        PhysiqueWeight = c.Int(nullable: false),
                        PhysiqueBMI = c.Double(nullable: false),
                        GynecologyVulva = c.String(maxLength: 100),
                        GynecologyVaginal = c.String(maxLength: 100),
                        GynecologyCervix = c.String(maxLength: 100),
                        GynecologyCervixBody = c.String(maxLength: 100),
                        GynecologyDoubleAtt = c.String(maxLength: 100),
                        TreatmentAdviceDiagnosis = c.String(maxLength: 100),
                        TreatmentAdvice = c.String(maxLength: 100),
                        DiagnosisDoctorId = c.Int(nullable: false),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBPatients", t => t.PatientId, cascadeDelete: true)
                .Index(t => t.PatientId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBMedicalDetails", "PatientId", "dbo.TBPatients");
            DropIndex("dbo.TBMedicalDetails", new[] { "PatientId" });
            DropTable("dbo.TBMedicalDetails");
        }
    }
}
