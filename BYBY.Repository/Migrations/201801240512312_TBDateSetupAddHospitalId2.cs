namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBDateSetupAddHospitalId2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TBDateSetups", "HospitalId", "dbo.TBHospitals");
            DropIndex("dbo.TBDateSetups", new[] { "HospitalId" });
            AlterColumn("dbo.TBDateSetups", "HospitalId", c => c.Int(nullable: false));
            CreateIndex("dbo.TBDateSetups", "HospitalId");
            AddForeignKey("dbo.TBDateSetups", "HospitalId", "dbo.TBHospitals", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBDateSetups", "HospitalId", "dbo.TBHospitals");
            DropIndex("dbo.TBDateSetups", new[] { "HospitalId" });
            AlterColumn("dbo.TBDateSetups", "HospitalId", c => c.Int());
            CreateIndex("dbo.TBDateSetups", "HospitalId");
            AddForeignKey("dbo.TBDateSetups", "HospitalId", "dbo.TBHospitals", "Id");
        }
    }
}
