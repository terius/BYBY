namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBMedicalDetailAddManInfos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBMedicalDetails", "PastMumps", c => c.String(maxLength: 100));
            AddColumn("dbo.TBMedicalDetails", "PastTesticularSurgery", c => c.String(maxLength: 100));
            AddColumn("dbo.TBMedicalDetails", "PastEpididymisSurgery", c => c.String(maxLength: 100));
            AddColumn("dbo.TBMedicalDetails", "PastVasectomy", c => c.String(maxLength: 100));
            AddColumn("dbo.TBMedicalDetails", "PastUrethralSurgery", c => c.String(maxLength: 100));
            AddColumn("dbo.TBMedicalDetails", "ManMarriageAge", c => c.Int());
            AddColumn("dbo.TBMedicalDetails", "ManLastMarriageDate", c => c.DateTime());
            AddColumn("dbo.TBMedicalDetails", "IsFemale", c => c.Boolean());
            AddColumn("dbo.TBMedicalDetails", "Title", c => c.String(maxLength: 50));
            AlterColumn("dbo.TBMedicalDetails", "PhysiqueHeight", c => c.Int());
            AlterColumn("dbo.TBMedicalDetails", "PhysiqueWeight", c => c.Int());
            AlterColumn("dbo.TBMedicalDetails", "PhysiqueBMI", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.TBMedicalDetails", "PhysiqueBMI", c => c.Double(nullable: false));
            AlterColumn("dbo.TBMedicalDetails", "PhysiqueWeight", c => c.Int(nullable: false));
            AlterColumn("dbo.TBMedicalDetails", "PhysiqueHeight", c => c.Int(nullable: false));
            DropColumn("dbo.TBMedicalDetails", "Title");
            DropColumn("dbo.TBMedicalDetails", "IsFemale");
            DropColumn("dbo.TBMedicalDetails", "ManLastMarriageDate");
            DropColumn("dbo.TBMedicalDetails", "ManMarriageAge");
            DropColumn("dbo.TBMedicalDetails", "PastUrethralSurgery");
            DropColumn("dbo.TBMedicalDetails", "PastVasectomy");
            DropColumn("dbo.TBMedicalDetails", "PastEpididymisSurgery");
            DropColumn("dbo.TBMedicalDetails", "PastTesticularSurgery");
            DropColumn("dbo.TBMedicalDetails", "PastMumps");
        }
    }
}
