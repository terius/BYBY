namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBMedicalHistoryAddEducation : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBPatients", "Education", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBPatients", "Education");
        }
    }
}
