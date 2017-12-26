namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTableTBConsultationMedicine : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TBConsultationMedicines",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MedicineId = c.Int(nullable: false),
                        ConsultationId = c.Int(nullable: false),
                        UsedDays = c.Int(nullable: false),
                        AllDose = c.Int(nullable: false),
                        ActionDate = c.DateTime(nullable: false),
                        AddTime = c.DateTime(),
                        ModifyTime = c.DateTime(),
                        AddUserName = c.String(maxLength: 20),
                        ModifyUserName = c.String(maxLength: 20),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TBConsultations", t => t.ConsultationId, cascadeDelete: true)
                .ForeignKey("dbo.TBMedicines", t => t.MedicineId, cascadeDelete: true)
                .Index(t => t.MedicineId)
                .Index(t => t.ConsultationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBConsultationMedicines", "MedicineId", "dbo.TBMedicines");
            DropForeignKey("dbo.TBConsultationMedicines", "ConsultationId", "dbo.TBConsultations");
            DropIndex("dbo.TBConsultationMedicines", new[] { "ConsultationId" });
            DropIndex("dbo.TBConsultationMedicines", new[] { "MedicineId" });
            DropTable("dbo.TBConsultationMedicines");
        }
    }
}
