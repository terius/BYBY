namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBConsultationAddPlanId2 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.TBConsultations", new[] { "PlanId" });
            AlterColumn("dbo.TBConsultations", "PlanId", c => c.Int(nullable: false));
            CreateIndex("dbo.TBConsultations", "PlanId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.TBConsultations", new[] { "PlanId" });
            AlterColumn("dbo.TBConsultations", "PlanId", c => c.Int());
            CreateIndex("dbo.TBConsultations", "PlanId");
        }
    }
}
