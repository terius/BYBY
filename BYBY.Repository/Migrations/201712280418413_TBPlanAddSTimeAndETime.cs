namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBPlanAddSTimeAndETime : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TBPlans", "DateSetupId", "dbo.TBDateSetups");
            DropIndex("dbo.TBPlans", new[] { "DateSetupId" });
            AddColumn("dbo.TBPlans", "STime", c => c.DateTime(nullable: false));
            AddColumn("dbo.TBPlans", "ETime", c => c.DateTime(nullable: false));
            DropColumn("dbo.TBPlans", "DateSetupId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TBPlans", "DateSetupId", c => c.Int(nullable: false));
            DropColumn("dbo.TBPlans", "ETime");
            DropColumn("dbo.TBPlans", "STime");
            CreateIndex("dbo.TBPlans", "DateSetupId");
            AddForeignKey("dbo.TBPlans", "DateSetupId", "dbo.TBDateSetups", "Id", cascadeDelete: true);
        }
    }
}
