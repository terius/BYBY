namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBReferralAddDoctorId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBReferrals", "DoctorId", c => c.Int());
            CreateIndex("dbo.TBReferrals", "DoctorId");
            AddForeignKey("dbo.TBReferrals", "DoctorId", "dbo.TBDoctors", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBReferrals", "DoctorId", "dbo.TBDoctors");
            DropIndex("dbo.TBReferrals", new[] { "DoctorId" });
            DropColumn("dbo.TBReferrals", "DoctorId");
        }
    }
}
