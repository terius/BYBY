namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class setTBMedicalHistoryLandlinePhonemaxlength50 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TBMedicalHistories", "LandlinePhone", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TBMedicalHistories", "LandlinePhone", c => c.String(maxLength: 30));
        }
    }
}
