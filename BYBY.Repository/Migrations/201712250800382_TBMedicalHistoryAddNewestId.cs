namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBMedicalHistoryAddNewestId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBMedicalHistories", "NewestConsultationId", c => c.Int());
            AddColumn("dbo.TBMedicalHistories", "NewestReferralId", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBMedicalHistories", "NewestReferralId");
            DropColumn("dbo.TBMedicalHistories", "NewestConsultationId");
        }
    }
}
