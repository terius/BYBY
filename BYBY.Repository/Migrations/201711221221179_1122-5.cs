namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _11225 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBMedicalHistories", "ConsultationStatus", c => c.Int(nullable: false));
            AddColumn("dbo.TBMedicalHistories", "ReferralStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBMedicalHistories", "ReferralStatus");
            DropColumn("dbo.TBMedicalHistories", "ConsultationStatus");
        }
    }
}
