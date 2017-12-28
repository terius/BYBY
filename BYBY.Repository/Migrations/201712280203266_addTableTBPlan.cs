namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableTBPlan : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateSetupId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        PlanDate = c.DateTime(nullable: false),
                        RoomId = c.Int(nullable: false),
                        People = c.Int(nullable: false),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBDateSetups", t => t.DateSetupId, cascadeDelete: true)
                .ForeignKey("dbo.TBDoctors", t => t.DoctorId, cascadeDelete: true)
                .ForeignKey("dbo.TBConsultationRooms", t => t.RoomId, cascadeDelete: true)
                .Index(t => t.DateSetupId)
                .Index(t => t.DoctorId)
                .Index(t => t.RoomId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBPlans", "RoomId", "dbo.TBConsultationRooms");
            DropForeignKey("dbo.TBPlans", "DoctorId", "dbo.TBDoctors");
            DropForeignKey("dbo.TBPlans", "DateSetupId", "dbo.TBDateSetups");
            DropIndex("dbo.TBPlans", new[] { "RoomId" });
            DropIndex("dbo.TBPlans", new[] { "DoctorId" });
            DropIndex("dbo.TBPlans", new[] { "DateSetupId" });
            DropTable("dbo.TBPlans");
        }
    }
}
