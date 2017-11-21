namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1113 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBMedicalHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicalHistoryNo = c.String(nullable: false, maxLength: 30),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                        FeMalePatient_Id = c.Int(),
                        MalePatient_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBPatients", t => t.FeMalePatient_Id)
                .ForeignKey("dbo.TBPatients", t => t.MalePatient_Id)
                .Index(t => t.FeMalePatient_Id)
                .Index(t => t.MalePatient_Id);
            
            CreateTable(
                "dbo.TBPatients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 20),
                        Birthday = c.DateTime(nullable: false),
                        Age = c.Int(),
                        CardType = c.Int(nullable: false),
                        CardNo = c.String(nullable: false, maxLength: 50),
                        MaritalStatus = c.Int(nullable: false),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBMedicalHistories", "MalePatient_Id", "dbo.TBPatients");
            DropForeignKey("dbo.TBMedicalHistories", "FeMalePatient_Id", "dbo.TBPatients");
            DropIndex("dbo.TBMedicalHistories", new[] { "MalePatient_Id" });
            DropIndex("dbo.TBMedicalHistories", new[] { "FeMalePatient_Id" });
            DropTable("dbo.TBPatients");
            DropTable("dbo.TBMedicalHistories");
        }
    }
}
