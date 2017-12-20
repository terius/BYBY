namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBHospitalAddParentHospital : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBHospitals", "ParentHospitalId", c => c.Int());
            CreateIndex("dbo.TBHospitals", "ParentHospitalId");
            AddForeignKey("dbo.TBHospitals", "ParentHospitalId", "dbo.TBHospitals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBHospitals", "ParentHospitalId", "dbo.TBHospitals");
            DropIndex("dbo.TBHospitals", new[] { "ParentHospitalId" });
            DropColumn("dbo.TBHospitals", "ParentHospitalId");
        }
    }
}
