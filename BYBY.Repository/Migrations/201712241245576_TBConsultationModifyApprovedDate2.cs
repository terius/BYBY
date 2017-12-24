namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBConsultationModifyApprovedDate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBConsultations", "ApprovedTime", c => c.DateTime());
            DropColumn("dbo.TBConsultations", "ApprovedDate");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TBConsultations", "ApprovedDate", c => c.DateTime());
            DropColumn("dbo.TBConsultations", "ApprovedTime");
        }
    }
}
