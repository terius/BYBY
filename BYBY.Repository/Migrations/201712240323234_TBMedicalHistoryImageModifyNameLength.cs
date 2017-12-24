namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBMedicalHistoryImageModifyNameLength : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.TBMedicalHistoryImages", "Name", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.TBMedicalHistoryImages", "FilePath", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TBMedicalHistoryImages", "FilePath", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.TBMedicalHistoryImages", "Name", c => c.String(nullable: false, maxLength: 30));
        }
    }
}
