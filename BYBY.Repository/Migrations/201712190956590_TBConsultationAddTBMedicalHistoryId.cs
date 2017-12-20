namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBConsultationAddTBMedicalHistoryId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBConsultations", "TBMedicalHistoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.TBConsultations", "TBMedicalHistoryId");
            AddForeignKey("dbo.TBConsultations", "TBMedicalHistoryId", "dbo.TBMedicalHistories", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBConsultations", "TBMedicalHistoryId", "dbo.TBMedicalHistories");
            DropIndex("dbo.TBConsultations", new[] { "TBMedicalHistoryId" });
            DropColumn("dbo.TBConsultations", "TBMedicalHistoryId");
        }
    }
}
