namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBConsultationRoomAddHospitalId2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TBConsultationRooms", "HospitalId", "dbo.TBHospitals");
            DropIndex("dbo.TBConsultationRooms", new[] { "HospitalId" });
            AlterColumn("dbo.TBConsultationRooms", "HospitalId", c => c.Int(nullable: false));
            CreateIndex("dbo.TBConsultationRooms", "HospitalId");
            AddForeignKey("dbo.TBConsultationRooms", "HospitalId", "dbo.TBHospitals", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBConsultationRooms", "HospitalId", "dbo.TBHospitals");
            DropIndex("dbo.TBConsultationRooms", new[] { "HospitalId" });
            AlterColumn("dbo.TBConsultationRooms", "HospitalId", c => c.Int());
            CreateIndex("dbo.TBConsultationRooms", "HospitalId");
            AddForeignKey("dbo.TBConsultationRooms", "HospitalId", "dbo.TBHospitals", "Id");
        }
    }
}
