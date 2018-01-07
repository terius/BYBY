namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBConsultationAddPlanId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBConsultations", "PlanId", c => c.Int());
            CreateIndex("dbo.TBConsultations", "PlanId");
            AddForeignKey("dbo.TBConsultations", "PlanId", "dbo.TBPlans", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TBConsultations", "PlanId", "dbo.TBPlans");
            DropIndex("dbo.TBConsultations", new[] { "PlanId" });
            DropColumn("dbo.TBConsultations", "PlanId");
        }
    }
}
