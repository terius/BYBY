namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBUserAddHospitalId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBUsers", "HospitalId", c => c.Int());
            CreateIndex("dbo.TBUsers", "HospitalId");
            AddForeignKey("dbo.TBUsers", "HospitalId", "dbo.TBHospitals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBUsers", "HospitalId", "dbo.TBHospitals");
            DropIndex("dbo.TBUsers", new[] { "HospitalId" });
            DropColumn("dbo.TBUsers", "HospitalId");
        }
    }
}
