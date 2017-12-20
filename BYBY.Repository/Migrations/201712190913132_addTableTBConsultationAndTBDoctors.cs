namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableTBConsultationAndTBDoctors : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBConsultations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        HospitalId = c.Int(nullable: false),
                        RequestDate = c.DateTime(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        STime = c.DateTime(nullable: false),
                        ETime = c.DateTime(nullable: false),
                        Remark = c.String(maxLength: 200),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBDoctors", t => t.DoctorId, cascadeDelete: true)
                .ForeignKey("dbo.TBHospitals", t => t.HospitalId, cascadeDelete: true)
                .Index(t => t.HospitalId)
                .Index(t => t.DoctorId);
            
            CreateTable(
                "dbo.TBDoctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 20),
                        JobTitle = c.String(maxLength: 50),
                        UserId = c.String(nullable: false, maxLength: 128),
                        HospitalId = c.Int(nullable: false),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBHospitals", t => t.HospitalId)
                .ForeignKey("dbo.TBUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.HospitalId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBConsultations", "HospitalId", "dbo.TBHospitals");
            DropForeignKey("dbo.TBConsultations", "DoctorId", "dbo.TBDoctors");
            DropForeignKey("dbo.TBDoctors", "UserId", "dbo.TBUsers");
            DropForeignKey("dbo.TBDoctors", "HospitalId", "dbo.TBHospitals");
            DropIndex("dbo.TBDoctors", new[] { "HospitalId" });
            DropIndex("dbo.TBDoctors", new[] { "UserId" });
            DropIndex("dbo.TBConsultations", new[] { "DoctorId" });
            DropIndex("dbo.TBConsultations", new[] { "HospitalId" });
            DropTable("dbo.TBDoctors");
            DropTable("dbo.TBConsultations");
        }
    }
}
