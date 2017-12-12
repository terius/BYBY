namespace BYBY.Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TBMedicalDetailsAddPersonalInfos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TBMedicalDetails", "PersonalSmoke", c => c.String(maxLength: 100));
            AddColumn("dbo.TBMedicalDetails", "PersonalAlcoholism", c => c.String(maxLength: 100));
            AddColumn("dbo.TBMedicalDetails", "PersonalDrug", c => c.String(maxLength: 100));
            AddColumn("dbo.TBMedicalDetails", "PersonalHabitMedication", c => c.String(maxLength: 100));
            AddColumn("dbo.TBMedicalDetails", "PersonalDrugAllergy", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TBMedicalDetails", "PersonalDrugAllergy");
            DropColumn("dbo.TBMedicalDetails", "PersonalHabitMedication");
            DropColumn("dbo.TBMedicalDetails", "PersonalDrug");
            DropColumn("dbo.TBMedicalDetails", "PersonalAlcoholism");
            DropColumn("dbo.TBMedicalDetails", "PersonalSmoke");
        }
    }
}
