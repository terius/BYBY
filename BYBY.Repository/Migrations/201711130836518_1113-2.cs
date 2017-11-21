namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11132 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TBMedicalHistories", new[] { "FeMalePatient_Id" });
            DropIndex("dbo.TBMedicalHistories", new[] { "MalePatient_Id" });
            RenameColumn(table: "dbo.TBMedicalHistories", name: "FeMalePatient_Id", newName: "FeMalePatientId");
            RenameColumn(table: "dbo.TBMedicalHistories", name: "MalePatient_Id", newName: "MalePatientId");
            AlterColumn("dbo.TBMedicalHistories", "FeMalePatientId", c => c.Int(nullable: false));
            AlterColumn("dbo.TBMedicalHistories", "MalePatientId", c => c.Int(nullable: false));
            CreateIndex("dbo.TBMedicalHistories", "MalePatientId");
            CreateIndex("dbo.TBMedicalHistories", "FeMalePatientId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TBMedicalHistories", new[] { "FeMalePatientId" });
            DropIndex("dbo.TBMedicalHistories", new[] { "MalePatientId" });
            AlterColumn("dbo.TBMedicalHistories", "MalePatientId", c => c.Int());
            AlterColumn("dbo.TBMedicalHistories", "FeMalePatientId", c => c.Int());
            RenameColumn(table: "dbo.TBMedicalHistories", name: "MalePatientId", newName: "MalePatient_Id");
            RenameColumn(table: "dbo.TBMedicalHistories", name: "FeMalePatientId", newName: "FeMalePatient_Id");
            CreateIndex("dbo.TBMedicalHistories", "MalePatient_Id");
            CreateIndex("dbo.TBMedicalHistories", "FeMalePatient_Id");
        }
    }
}
