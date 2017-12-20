namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBReferralAndTBConsultationAddStatus : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBConsultations", "ConsultationStatus", c => c.Int(nullable: false));
            AddColumn("dbo.TBReferrals", "ReferralStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBReferrals", "ReferralStatus");
            DropColumn("dbo.TBConsultations", "ConsultationStatus");
        }
    }
}
