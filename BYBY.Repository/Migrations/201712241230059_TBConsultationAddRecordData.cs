namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBConsultationAddRecordData : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBConsultations", "Diagnosis", c => c.String(maxLength: 500));
            AddColumn("dbo.TBConsultations", "TreatmentAdvice", c => c.String(maxLength: 500));
            AddColumn("dbo.TBConsultations", "TreatmentRemark", c => c.String(maxLength: 500));
            AddColumn("dbo.TBConsultations", "RecordUser", c => c.String(maxLength: 100));
            AddColumn("dbo.TBConsultations", "RecordTime", c => c.DateTime());
            AlterColumn("dbo.TBConsultations", "ApprovedUser", c => c.String(maxLength: 100));
            AlterColumn("dbo.TBConsultations", "ApprovedTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TBConsultations", "ApprovedTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.TBConsultations", "ApprovedUser", c => c.String());
            DropColumn("dbo.TBConsultations", "RecordTime");
            DropColumn("dbo.TBConsultations", "RecordUser");
            DropColumn("dbo.TBConsultations", "TreatmentRemark");
            DropColumn("dbo.TBConsultations", "TreatmentAdvice");
            DropColumn("dbo.TBConsultations", "Diagnosis");
        }
    }
}
