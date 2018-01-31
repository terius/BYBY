namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBDateSetupAddHospitalId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBDateSetups", "HospitalId", c => c.Int());
            CreateIndex("dbo.TBDateSetups", "HospitalId");
            AddForeignKey("dbo.TBDateSetups", "HospitalId", "dbo.TBHospitals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBDateSetups", "HospitalId", "dbo.TBHospitals");
            DropIndex("dbo.TBDateSetups", new[] { "HospitalId" });
            DropColumn("dbo.TBDateSetups", "HospitalId");
        }
    }
}
