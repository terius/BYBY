namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBConsultationRoomAddHospitalId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBConsultationRooms", "HospitalId", c => c.Int());
            CreateIndex("dbo.TBConsultationRooms", "HospitalId");
            AddForeignKey("dbo.TBConsultationRooms", "HospitalId", "dbo.TBHospitals", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBConsultationRooms", "HospitalId", "dbo.TBHospitals");
            DropIndex("dbo.TBConsultationRooms", new[] { "HospitalId" });
            DropColumn("dbo.TBConsultationRooms", "HospitalId");
        }
    }
}
