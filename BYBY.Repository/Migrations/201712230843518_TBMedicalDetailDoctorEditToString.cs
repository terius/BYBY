namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBMedicalDetailDoctorEditToString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBMedicalDetails", "DiagnosisDoctor", c => c.String(maxLength: 50));
            DropColumn("dbo.TBMedicalDetails", "DiagnosisDoctorId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TBMedicalDetails", "DiagnosisDoctorId", c => c.Int(nullable: false));
            DropColumn("dbo.TBMedicalDetails", "DiagnosisDoctor");
        }
    }
}
