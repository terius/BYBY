namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBReferralAddDoctorId2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TBReferrals", "DoctorId", "dbo.TBDoctors");
            DropIndex("dbo.TBReferrals", new[] { "DoctorId" });
            AlterColumn("dbo.TBReferrals", "DoctorId", c => c.Int(nullable: false));
            CreateIndex("dbo.TBReferrals", "DoctorId");
            AddForeignKey("dbo.TBReferrals", "DoctorId", "dbo.TBDoctors", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBReferrals", "DoctorId", "dbo.TBDoctors");
            DropIndex("dbo.TBReferrals", new[] { "DoctorId" });
            AlterColumn("dbo.TBReferrals", "DoctorId", c => c.Int());
            CreateIndex("dbo.TBReferrals", "DoctorId");
            AddForeignKey("dbo.TBReferrals", "DoctorId", "dbo.TBDoctors", "Id");
        }
    }
}
