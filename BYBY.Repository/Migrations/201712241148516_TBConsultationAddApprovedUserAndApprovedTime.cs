namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBConsultationAddApprovedUserAndApprovedTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBConsultations", "ApprovedUser", c => c.String());
            AddColumn("dbo.TBConsultations", "ApprovedTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBConsultations", "ApprovedTime");
            DropColumn("dbo.TBConsultations", "ApprovedUser");
        }
    }
}
